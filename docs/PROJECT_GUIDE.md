# Registration Module — Complete Project Guide

A deep, single-document explanation of the whole task: **what it does, how the data
flows, every concept and pattern, every library, every algorithm, and every tool/CLI**
used to build and verify it.

> If you only read one doc, read this. The root [README](../README.md) is the quick
> start; [acceptance-criteria.md](acceptance-criteria.md) maps the spec to the code.

---

## Table of contents

1. [What the task is](#1-what-the-task-is)
2. [The big picture (architecture)](#2-the-big-picture-architecture)
3. [End-to-end flow of a registration](#3-end-to-end-flow-of-a-registration)
4. [Concepts & design patterns](#4-concepts--design-patterns)
5. [Algorithms & business logic](#5-algorithms--business-logic)
6. [Backend libraries (NuGet)](#6-backend-libraries-nuget)
7. [Frontend libraries (npm)](#7-frontend-libraries-npm)
8. [Tools & CLIs used to build and verify](#8-tools--clis-used-to-build-and-verify)
9. [Validation rules reference](#9-validation-rules-reference)
10. [API reference](#10-api-reference)
11. [Data model & database](#11-data-model--database)
12. [Testing strategy](#12-testing-strategy)
13. [CI/CD](#13-cicd)
14. [Glossary](#14-glossary)

---

## 1. What the task is

Build a **registration module**: a user creates a profile with personal details and
**one to five addresses** (exactly one primary). The UI is **React + TypeScript**; the
data is persisted through a **.NET 8 REST API** built with **Clean Architecture, CQRS,
validation, and SQL Server**. Bonus goals (all implemented): RabbitMQ/MassTransit with
the **Outbox pattern**, **Docker Compose**, structured logging with correlation IDs,
health checks, integration tests, frontend E2E tests, and a pagination/search endpoint.

The system must:

- Validate on both client and server (defense in depth).
- Enforce unique **Email** and **Mobile number**.
- Filter **City** by the selected **Governorate** (dependent dropdowns) and enforce the
  same rule server-side.
- Return **201** with a `Location` header on success, **400** for validation, **409** for
  duplicates, using a consistent **ProblemDetails** shape.
- Do post-registration work (welcome email/SMS, audit) **asynchronously** without making
  the create transaction depend on message delivery.

---

## 2. The big picture (architecture)

The backend uses **Clean Architecture**: code is split into concentric layers, and
**dependencies only point inward**. The innermost layer (Domain) knows nothing about
databases, web frameworks, or messaging.

```
        ┌───────────────────────────────────────────────────────────┐
        │                     Registration.Api                       │  Presentation
        │   Controllers · Swagger · ProblemDetails · Serilog · CORS  │  (composition root)
        └───────────────┬───────────────────────────┬───────────────┘
                        │                           │
        ┌───────────────▼─────────────┐ ┌───────────▼───────────────┐
        │   Registration.Persistence  │ │ Registration.Infrastructure│
        │   EF Core · Migrations ·     │ │ MassTransit · RabbitMQ ·   │
        │   Interceptors · Outbox tbls │ │ Outbox delivery · Email/SMS│
        └───────────────┬─────────────┘ └───────────┬───────────────┘
                        │                           │
                        └─────────────┬─────────────┘
                                      │ (both implement Application interfaces)
                        ┌─────────────▼─────────────┐
                        │   Registration.Application │  Use cases
                        │   CQRS · MediatR · DTOs ·  │
                        │   FluentValidation · Maps  │
                        └─────────────┬─────────────┘
                                      │
                        ┌─────────────▼─────────────┐
                        │     Registration.Domain    │  Enterprise rules
                        │  Entities · Value objects · │  (no dependencies)
                        │  Aggregates · Domain events │
                        └───────────────────────────┘
```

**The dependency rule** is enforced by project references: `Domain` has *zero* package
or project references. `Application` references only `Domain`. `Persistence` and
`Infrastructure` implement interfaces defined in `Application`. `Api` is the
**composition root** — the only place that wires concrete implementations to interfaces.

Why this matters: business rules don't change when you swap SQL Server for Postgres, or
RabbitMQ for Azure Service Bus. The Domain and Application layers are framework-agnostic
and therefore easy to unit-test in isolation.

---

## 3. End-to-end flow of a registration

```
React form (RHF + Zod)                 .NET 8 API                         SQL Server / RabbitMQ
──────────────────────                ───────────────                    ──────────────────────
1. User fills form
2. Zod validates on the client  ─────▶ POST /api/registrations (JSON)
                                       3. Correlation-Id middleware tags the request
                                       4. MediatR pipeline:
                                          a. UnhandledException behavior
                                          b. Logging behavior (name + duration)
                                          c. Validation behavior → FluentValidation
                                             - field rules + async DB checks
                                             - city-belongs-to-governorate
                                       5. CreateRegistrationCommandHandler
                                          a. build value objects (PersonName, Email,
                                             MobileNumber, BirthDate) — throws if invalid
                                          b. uniqueness check (email/mobile) → 409
                                          c. Registration.Create(...) → enforces
                                             invariants + raises domain event
                                          d. db.Registrations.Add(entity)
                                          e. publish RegistrationCreatedIntegrationEvent
                                             ──────────────────────────────────────────▶ stored in OUTBOX table
                                          f. db.SaveChangesAsync()  ───────────────────▶ INSERT (one transaction:
                                             - AuditableEntityInterceptor sets audit cols    aggregate + outbox row)
                                             - DispatchDomainEventsInterceptor → MediatR
                                       6. 201 Created + Location header + { id }
                                                                                          7. Outbox delivery service
                                                                                             publishes to RabbitMQ
                                                                                          8. Consumers run:
                                                                                             - SendWelcomeNotification
                                                                                               (email + SMS)
                                                                                             - RegistrationAudit
8. UI shows the created id ◀───────── (or 400/409 mapped to fields)
```

The **key reliability property**: the integration event is written to the *outbox table*
in the **same database transaction** as the registration. If RabbitMQ is down, the
registration still succeeds; the message is delivered later by the outbox processor. This
is the **Transactional Outbox** pattern.

---

## 4. Concepts & design patterns

### Clean Architecture
Layering with an inward dependency rule (above). Goal: keep business rules independent of
frameworks, UI, and databases.

### Domain-Driven Design (DDD) building blocks
- **Entity** — an object with identity that persists over time (`Registration`, `Address`).
  Equality is by id, not by attribute values.
- **Value Object** — an immutable object defined by its values, with **structural
  equality** (`PersonName`, `Email`, `MobileNumber`, `BirthDate`). Two emails are equal if
  their normalized values match. Value objects validate themselves on creation, so they can
  never exist in an invalid state.
- **Aggregate / Aggregate Root** — a cluster of objects treated as one consistency
  boundary. `Registration` is the root; `Address` only exists inside it. You always load
  and save the whole aggregate, and **invariants** (rules that must always hold) are
  enforced by the root's factory (`Registration.Create`).
- **Domain Event** — a fact that happened in the domain (`RegistrationCreatedDomainEvent`).
  Raised by the aggregate, dispatched in-process after save.
- **Invariant** — a rule that must always be true (e.g., "1–5 addresses, exactly one
  primary"). Enforced in the domain so it can't be bypassed.
- **Factory method** — `Registration.Create(...)` / `Email.Create(...)` build valid objects
  and centralize validation, instead of public constructors that could create invalid state.

### CQRS (Command Query Responsibility Segregation)
Writes (**Commands**, e.g. `CreateRegistrationCommand`) and reads (**Queries**, e.g.
`GetRegistrationByIdQuery`, `ListRegistrationsQuery`) are separate types with separate
handlers. Reads can be optimized (projections, no tracking) independently of writes.

### Mediator pattern (MediatR)
Controllers don't call handlers directly; they `Send` a request through a **mediator**,
which routes it to the matching handler. This decouples the API from the application logic
and enables a **pipeline**.

### Pipeline behaviors (a.k.a. middleware / decorator pattern)
Cross-cutting concerns wrap every request in order:
`UnhandledException → Logging → Validation → handler`. Each behavior can run code before
and after the next one. This is the **Chain of Responsibility / Decorator** pattern applied
to use cases. Validation runs *before* the handler, so handlers always receive valid input.

### Validation & "defense in depth"
The same rules are checked on the **client** (Zod) for fast feedback and on the **server**
(FluentValidation + domain invariants) for security — a client can be bypassed, the server
can't. Uniqueness is intentionally **not** a validation error (400); it's a **409 Conflict**.

### Mapping (AutoMapper) + deliberate manual mapping
Domain → DTO projection uses **AutoMapper** (and its config is unit-tested with
`AssertConfigurationIsValid`). Inbound mapping (request → domain) is done **by hand** in the
handler because building value objects needs their factory methods and the injected "today".

### Persistence abstraction (`IApplicationDbContext`)
The Application layer defines the persistence **contract** (`DbSet<T>` + `SaveChangesAsync`);
Persistence implements it with EF Core. Handlers depend on the interface, not on a concrete
`DbContext`. (This is the reference Clean Architecture approach; an alternative is the
Repository + Unit-of-Work pattern.)

### Unit of Work
EF Core's `DbContext` *is* a Unit of Work: it tracks changes and commits them atomically in
one `SaveChangesAsync`. The registration insert and the outbox row commit together.

### Interceptors (EF Core `SaveChangesInterceptor`)
- **AuditableEntityInterceptor** — fills `CreatedAtUtc/By`, `UpdatedAtUtc/By` automatically.
- **DispatchDomainEventsInterceptor** — after a successful save, publishes the aggregate's
  domain events through MediatR. Keeps the domain clean of dispatch concerns.

### Domain events vs. Integration events
- **Domain event** = in-process, same service, after commit (e.g. structured logging).
- **Integration event** = cross-service, over a message broker (e.g. welcome email/SMS).
  Published through the outbox.

### Transactional Outbox pattern
Instead of publishing to RabbitMQ directly (which could fail after the DB commit, or commit
after a publish that then rolls back — the **dual-write problem**), the message is saved to
an **outbox table** in the same transaction. A background **outbox delivery service**
(MassTransit) reads unsent messages and publishes them, marking them delivered. Guarantees
**at-least-once** delivery and consistency.

### Publish/Subscribe messaging
The API **publishes** an event; any number of **consumers** subscribe independently
(`SendWelcomeNotificationConsumer`, `RegistrationAuditConsumer`). Adding a new reaction
doesn't touch the publisher.

### Dependency Injection (DI) & composition root
Each layer exposes an `Add<Layer>()` extension that registers its services. The API's
`Program.cs` (the **composition root**) calls them and is the only place that knows the
concrete types. Promotes the **Inversion of Control** principle.

### Options pattern
External config (`RabbitMqOptions`) is bound from configuration into a strongly-typed class
and injected, instead of reading magic strings everywhere.

### ProblemDetails (RFC 7807)
A standard JSON error shape (`type`, `title`, `status`, `detail`, plus `errors` for
validation and a `correlationId`). One `GlobalExceptionHandler` maps exceptions to status
codes consistently.

### Correlation ID
A per-request id (`X-Correlation-ID`) is generated or read from the header, echoed back, and
pushed into the Serilog **log context** so every log line for that request is traceable.

### Structured logging
Logs are events with named properties (not just text), making them queryable. **No personal
data** is logged — only request names, durations, ids, and outcomes.

### Health checks
`/health` aggregates the SQL Server check and the MassTransit/RabbitMQ bus check into one
endpoint for orchestrators/monitors.

### Central Package Management (CPM)
All NuGet versions live in one `Directory.Packages.props`; project files reference packages
**without versions**. Keeps versions consistent and upgrades in one place.

### Database migrations & seeding
**Migrations** are versioned C# files that evolve the schema reproducibly. **Seeding**
(`HasData`) inserts the fixed Governorate/City lookups as part of a migration.

---

## 5. Algorithms & business logic

### Accurate age calculation
Naïve `year diff` is wrong near birthdays. The algorithm subtracts a year if the birthday
hasn't occurred yet this year:
```
age = today.Year - birth.Year
if (birth > today.AddYears(-age)) age--   // birthday not reached yet
```
Used by `BirthDate` (domain), the Zod schema (client), and tested across the boundary
(birthday today vs. tomorrow). Minimum age = **20**.

### E.164 mobile normalization & validation
Real input contains spaces/dashes/parentheses. We **strip separators** then validate against
`^\+[1-9]\d{7,14}$` (a `+`, a non-zero first digit, total 8–15 digits). The normalized form
is stored and uniquely indexed, so `+20 100 615 8123` and `+201006158123` are the same.

### Email normalization (case-insensitive uniqueness)
The original (trimmed) value is kept for display; a `Normalized = value.ToLowerInvariant()`
form is stored in a separate column with a **unique index**, so `A@x.com` and `a@x.com`
collide.

### Name normalization & validation (Arabic + English)
Normalize = `trim` then collapse repeated whitespace to a single space (regex `\s+` → `" "`).
Validate against `^[A-Za-z؀-ۿ]+(?:[ '\-][A-Za-z؀-ۿ]+)*$` — one or more
Arabic (`؀–ۿ`) or English letters, joined by single space/hyphen/apostrophe. This
accepts `Al-Sayed`, `O'Brien`, `عبد الرحمن`, and rejects digits/specials. Max 50 chars.

### Building/flat number rule
`^[A-Za-z0-9/\- ]+$` — allows realistic values like `12A` and `10/2`.

### Single-primary-address enforcement
- If exactly **one** address → it is forced primary.
- If **multiple** → exactly one must be marked primary, else the domain throws / the schema
  fails. Enforced in `Registration.SetAddresses` (server) and the Zod `superRefine` (client).

### Duplicate detection with a race-safe backstop
The handler first checks `AnyAsync(email/mobile)` for a friendly 409. Two concurrent inserts
could both pass that check, so the DB **unique index** is the source of truth: on
`DbUpdateException`, the handler re-checks and re-throws the correct 409 (provider-agnostic).

### Dependent dropdown (City ⇐ Governorate)
On the client, changing the Governorate select resets the City and fetches
`/api/lookups/cities?governorateId=…`. Cities are cached **per address row by field id** so
add/remove doesn't mix them up. The server independently verifies the city belongs to the
governorate (`MustAsync`).

### Pagination math
`PagedResult<T>` computes `TotalPages = ceil(TotalCount / PageSize)` and
`HasNext/Previous`. The query applies `Skip((page-1)*pageSize).Take(pageSize)` over an
ordered, filtered `IQueryable`, so paging happens in SQL.

### Owned value objects + EF constructor binding
Value objects have no public setters; EF materializes them through their constructors. EF
only auto-discovers some properties, so each owned property is mapped **explicitly**
(`.Property(...)`) — required for get-only properties to bind.

---

## 6. Backend libraries (NuGet)

| Library | Layer | What it does |
| --- | --- | --- |
| **MediatR** 12 | Application | In-process mediator for CQRS + pipeline behaviors. |
| **FluentValidation** 11 | Application | Declarative, testable validators (incl. async DB rules). |
| **AutoMapper** 13 | Application | Object-to-object mapping (domain → DTO) with a testable profile. |
| **Microsoft.EntityFrameworkCore** 8 (+ SqlServer, Relational, Design) | App/Persistence | ORM: LINQ queries, change tracking, migrations, SQL Server provider. |
| **MassTransit** 8 (+ RabbitMQ, EntityFrameworkCore) | Infrastructure | Message-bus abstraction over RabbitMQ + the EF Core transactional outbox. |
| **Serilog.AspNetCore** 8 (+ enrichers) | Api | Structured logging, request logging, log-context enrichment. |
| **Swashbuckle.AspNetCore** 6 (+ .Filters) | Api | Swagger/OpenAPI generation + request/response examples. |
| **AspNetCore.HealthChecks.SqlServer / .UI.Client** | Api | `/health` checks + JSON response writer. (RabbitMQ health comes from MassTransit.) |
| **Microsoft.Extensions.\*** (DI, Logging, Configuration, Options, Hosting) | all | The .NET hosting/DI/config/logging abstractions. |
| **xUnit** | tests | Test framework. |
| **FluentAssertions** | tests | Readable assertions (`x.Should().Be(...)`). |
| **NSubstitute** | tests | Mocking/substitutes for interfaces. |
| **Microsoft.EntityFrameworkCore.InMemory** | tests | Fast in-memory EF provider for Application unit tests. |
| **Microsoft.AspNetCore.Mvc.Testing** | tests | `WebApplicationFactory` to host the API in-process for integration tests. |
| **Testcontainers.MsSql** | tests | Spins up a real SQL Server 2019 container for integration tests. |
| **Microsoft.Data.SqlClient** (transitive) | runtime | The actual SQL Server driver used by EF Core. |

---

## 7. Frontend libraries (npm)

| Library | What it does |
| --- | --- |
| **React** 18 | Component-based UI library. |
| **TypeScript** 5 | Static typing over JavaScript. |
| **Vite** 5 | Dev server (HMR) + production bundler; `/api` proxy in dev. |
| **react-hook-form** 7 | Performant form state with minimal re-renders; `useForm`, `useFieldArray`. |
| **@hookform/resolvers** | Adapter so RHF validates with a Zod schema. |
| **Zod** 3 | Schema declaration + validation; mirrors the backend rules. |
| **Vitest** 2 | Vite-native unit-test runner (Jest-compatible API). |
| **@testing-library/react** + **jest-dom** + **user-event** | Component testing from the user's perspective + DOM matchers. |
| **jsdom** | Headless DOM for component tests. |
| **@playwright/test** | End-to-end browser testing (happy path + validation failure). |

---

## 8. Tools & CLIs used to build and verify

These are the actual command-line tools and services used to create, build, run, and verify
the project.

| Tool | Used for |
| --- | --- |
| **.NET SDK / `dotnet` CLI** | `dotnet new` (solution + projects), `add reference`, `restore`, `build`, `test`, `publish`. |
| **`dotnet-ef`** (pinned 8.0.11 local tool) | `migrations add InitialCreate`, `database update` — generate/apply EF Core migrations. |
| **MSBuild** (via `dotnet`) | Builds the projects; reads `Directory.Build.props` / `Directory.Packages.props`. |
| **NuGet** (Central Package Management) | Package restore; `NuGetAuditSuppress` for a single risk-accepted advisory. |
| **Node.js + `npm`** | `npm ci/install`, `npm test`, `npm run build`, `npm run e2e` for the frontend. |
| **Vite / Vitest / Playwright** | Frontend build, unit tests, and E2E tests. |
| **Git** | Version control; feature branches + merge commits. |
| **GitHub REST API** (via `curl`) | Create the repo, open PRs, merge PRs (9 PRs total). Auth via **Git Credential Manager** (no token in files). |
| **Docker + Docker Compose** | Run SQL Server 2019 + RabbitMQ + API + frontend; build images; verify end-to-end. |
| **`sqlcmd`** | Inspect the database (tables, indexes, seed) and verify migrations applied. |
| **GitHub Actions** | CI: build + unit + integration tests + frontend + docker image builds. |
| **Python (PyMuPDF / `fitz`)** | Extracted the task description text from the provided PDF at the start. |
| **nginx** | Serves the built SPA and reverse-proxies `/api` to the API container in Docker. |

### How the layers were assembled (build order)
Each layer was built, tested, committed on its own branch, and merged via PR, so the git
history reads like the architecture: **Domain → Application → Persistence → Infrastructure →
API → Integration tests → Frontend → Docker → CI**. The Domain/Application layers were
proven with unit tests before any database existed; the database and messaging were verified
with a live Docker stack; the whole flow was confirmed through the nginx-proxied UI.

---

## 9. Validation rules reference

| Field | Rule (enforced client **and** server) |
| --- | --- |
| First name | required; ≤ 50; Arabic/English letters + space, hyphen, apostrophe; trim + collapse spaces. |
| Middle name | optional; same character rules when present. |
| Last name | required; same rules as first name. |
| Birth date | date only; not in the future; **age ≥ 20** (accurate calc). |
| Mobile | required; valid **E.164**; stored/compared normalized; **unique**. |
| Email | required; valid format; ≤ 254; **unique, case-insensitive** (normalized column). |
| Governorate | required; must exist (active). |
| City | required; must exist (active) **and belong to the selected governorate**. |
| Street | required; ≤ 200; trimmed. |
| Building / Flat number | required; ≤ 20; letters, digits, `/`, `-`, space (e.g. `12A`, `10/2`). |
| Addresses | 1–5; exactly one primary (auto-primary when only one). |

Uniqueness violations are **409 Conflict** (not 400). Invalid governorate/city combinations
are **400**.

---

## 10. API reference

| Method | Route | Success | Errors |
| --- | --- | --- | --- |
| POST | `/api/registrations` | **201** + `Location` + `{ id }` | 400, 409 |
| GET | `/api/registrations/{id}` | 200 (details + lookup names + audit) | 404 |
| GET | `/api/registrations?page=&pageSize=&search=` | 200 paged list | 400 |
| GET | `/api/lookups/governorates` | 200 (active) | — |
| GET | `/api/lookups/cities?governorateId=` | 200 (active, filtered) | 400 |

All errors use the **ProblemDetails** shape with a `correlationId`. Swagger UI at `/swagger`
documents the contract with request/response examples.

---

## 11. Data model & database

Tables (SQL Server 2019, created by the `InitialCreate` migration):

- **Registrations** — id (GUID), owned columns `FirstName/MiddleName/LastName`, `BirthDate`,
  `Email` + `NormalizedEmail`, `Mobile`, audit columns. Unique indexes
  `UX_Registrations_NormalizedEmail`, `UX_Registrations_Mobile`.
- **Addresses** — id (GUID), FK `RegistrationId` (cascade), `GovernorateId`/`CityId`
  (restrict FKs), street/building/flat, `IsPrimary`.
- **Governorates** / **Cities** — bilingual (EN/AR) lookups; City → Governorate FK. Seeded
  with 3 governorates and 8 cities via `HasData`.
- **OutboxMessage / OutboxState / InboxState** — MassTransit transactional outbox tables.

The value objects are mapped as EF Core **owned types** (they share the `Registrations`
table), with each property mapped explicitly so EF can bind their constructors.

---

## 12. Testing strategy

| Level | Project / location | What it proves |
| --- | --- | --- |
| **Domain unit** (52) | `Registration.Domain.UnitTests` | Value-object rules, accurate age, aggregate invariants. |
| **Application unit** (19) | `Registration.Application.UnitTests` | Validators, command handler (happy + duplicate), **mapping config validity**, validation behavior. Uses EF InMemory. |
| **Integration** | `Registration.IntegrationTests` | Real HTTP via `WebApplicationFactory` + a real SQL Server 2019 (Testcontainers locally / a service container in CI). Covers create→201, duplicate→409, city-mismatch→400, lookups, 404. In-memory message transport so no broker is needed. |
| **Frontend unit** (21) | `frontend/src/**/*.test.ts(x)` | Zod schema (Arabic names, age, E.164, single-primary), `TextInput` accessibility. |
| **E2E** | `frontend/e2e` | Playwright: happy path + validation-failure path. |

The mapping configuration is explicitly tested with AutoMapper's
`AssertConfigurationIsValid()` (an acceptance criterion). Application handler/validator tests
run against EF Core InMemory so they're fast and need no database.

---

## 13. CI/CD

GitHub Actions (`.github/workflows/ci.yml`) runs on every push to `main` and every PR:

- **backend** — restore, build, Domain + Application unit tests, then integration tests
  against a **SQL Server 2019 service container** (the test factory targets it via the
  `INTEGRATION_TEST_DB` environment variable; EF Core creates the database on first migrate).
- **frontend** — `npm ci`, Vitest, production build.
- **docker** — builds the API and frontend images via Compose.

The whole codebase was delivered through **9 reviewed pull requests** with a green CI badge.

---

## 14. Glossary

- **Aggregate** — a consistency boundary of objects saved/loaded together.
- **CQRS** — separate models for commands (writes) and queries (reads).
- **DTO** — Data Transfer Object; the shape returned to clients (decoupled from entities).
- **Dual-write problem** — the risk of writing to a DB and a broker non-atomically; solved by
  the outbox.
- **E.164** — the international phone-number format (`+` then up to 15 digits).
- **Idempotent** — safe to apply repeatedly (e.g. re-running migrations).
- **Invariant** — a rule that must always hold for an aggregate.
- **Mediator** — routes a request to its handler, decoupling caller from callee.
- **Owned type** — an EF Core value object mapped into its owner's table.
- **ProblemDetails** — RFC 7807 standard error body.
- **Seeding** — inserting fixed reference data via a migration.
- **Transactional Outbox** — store-then-forward messaging for atomic DB + broker writes.
- **Value object** — an immutable, self-validating object compared by value.
```
