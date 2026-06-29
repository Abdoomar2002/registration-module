# Registration Module

A full-stack **user registration** module built for the 3S Group developer task.

- **Frontend:** React + TypeScript (Vite), React Hook Form + Zod, accessible reusable components.
- **Backend:** .NET 8 REST API using **Clean Architecture**, **CQRS** (MediatR), **FluentValidation**, **AutoMapper**, **EF Core / SQL Server 2019**.
- **Messaging (bonus):** RabbitMQ + MassTransit with the **Transactional Outbox** pattern for reliable post-registration processing.
- **Ops (bonus):** Docker Compose, Serilog structured logging with correlation IDs, health checks, unit + integration + E2E tests.

> Built incrementally via feature branches → pull requests (Domain → Application → Persistence →
> Infrastructure → API → Integration tests → Frontend → Docker). See the
> [merged PRs](https://github.com/Abdoomar2002/registration-module/pulls?q=is%3Apr+is%3Aclosed) for the layer-by-layer history.

---

## Solution at a glance

A user fills a registration form (personal details + 1–5 addresses, exactly one primary).
The API validates the request on the server (mirroring the client rules), persists it to SQL Server,
enforces uniqueness of email/mobile, and asynchronously publishes a `RegistrationCreated`
integration event through an outbox so a welcome email/SMS and an audit record can be produced
without coupling them to the create transaction.

```
                 ┌──────────────┐      POST /api/registrations        ┌────────────────────┐
  React + TS  ───▶   REST API   │ ───────────────────────────────────▶  Application (CQRS) │
  (Vite/nginx)◀───   (.NET 8)   │   201 Created + Location header     │  + FluentValidation │
                 └──────┬───────┘                                     └─────────┬──────────┘
                        │ EF Core                                               │ publish (outbox)
                        ▼                                                       ▼
                 ┌──────────────┐        outbox            ┌────────────┐   ┌─────────────────┐
                 │ SQL Server   │ ───────────────────────▶ │  RabbitMQ  │──▶│ Welcome / Audit  │
                 │   2019       │   (MassTransit outbox)    │ MassTransit│   │   consumers      │
                 └──────────────┘                          └────────────┘   └─────────────────┘
```

## Quick start (Docker Compose — recommended)

Requires Docker. From the repository root:

```bash
docker compose -f docker/docker-compose.yml up --build
```

This starts SQL Server 2019, RabbitMQ, the API (which auto-applies EF Core migrations and seeds
lookups on startup), and the frontend behind nginx.

| Service | URL | Notes |
| --- | --- | --- |
| **Frontend** | http://localhost:8081 | The registration form |
| **API** | http://localhost:8080 | REST API |
| **Swagger** | http://localhost:8080/swagger | Endpoints + request/response examples |
| **Health** | http://localhost:8080/health | SQL Server + RabbitMQ/bus |
| **RabbitMQ UI** | http://localhost:15672 | guest / guest |
| **SQL Server** | localhost,14333 | sa / `Your_strong!Passw0rd` (override via `SA_PASSWORD`) |

Tear down (and drop the database volume):

```bash
docker compose -f docker/docker-compose.yml down -v
```

## Local development (without Docker)

- **API:** needs the .NET 8 SDK and a reachable SQL Server. Set the connection string and run:
  ```bash
  cd backend
  dotnet tool restore                                   # restores the pinned dotnet-ef
  dotnet ef database update --project src/Registration.Persistence --startup-project src/Registration.Persistence
  dotnet run --project src/Registration.Api            # https/http per launchSettings
  ```
  Configure `ConnectionStrings:Default` and `RabbitMq:*` via `appsettings.Development.json`, user
  secrets, or environment variables (`ConnectionStrings__Default`, `RabbitMq__Host`, …).
- **Frontend:** needs Node 20+.
  ```bash
  cd frontend
  npm install
  npm run dev          # http://localhost:5173, proxies /api -> VITE_API_PROXY_TARGET
  ```

## Architecture (Clean Architecture)

Dependencies point **inward** only. The Domain has no knowledge of EF Core, MediatR, AutoMapper,
or any framework.

| Project | Responsibility | May depend on |
| --- | --- | --- |
| `Registration.Domain` | Entities, value objects, domain events, invariants. | *(nothing)* |
| `Registration.Application` | CQRS commands/queries, MediatR handlers, DTOs, FluentValidation, AutoMapper profiles, interfaces, pipeline behaviors. | Domain |
| `Registration.Persistence` | EF Core `DbContext`, Fluent configs, migrations, seed data, interceptors (audit + outbox). | Application, Domain |
| `Registration.Infrastructure` | RabbitMQ/MassTransit, outbox delivery, email/SMS, DI for integrations. | Application, Domain, Persistence |
| `Registration.Api` | ASP.NET Core controllers, Swagger, ProblemDetails, correlation middleware, Serilog, health checks. Composition root. | Application, Infrastructure, Persistence |

The dependency direction is enforced by project references: **Domain has no dependency on Persistence,
Infrastructure, or Presentation.**

## API

| Method | Route | Result |
| --- | --- | --- |
| POST | `/api/registrations` | **201 Created** + `Location` header + `{ id }` |
| GET | `/api/registrations/{id}` | 200 registration details (with lookup names + audit) / 404 |
| GET | `/api/registrations?page=&pageSize=&search=` | paged + searchable list (bonus) |
| GET | `/api/lookups/governorates` | active governorates |
| GET | `/api/lookups/cities?governorateId=` | active cities for a governorate |

Errors use a consistent **RFC 7807 ProblemDetails** shape with a `correlationId`:
**400** validation / domain-rule, **404** not found, **409** duplicate email or mobile, **500** unexpected.

## Validation rules (enforced on the server, mirrored on the client)

- **Names** (first required, middle optional, last required): Arabic or English letters, spaces, hyphen,
  apostrophe; trimmed and repeated spaces collapsed; max 50.
- **Birth date**: date only, not in the future, **age ≥ 20** (calculated accurately, not by year diff).
- **Mobile**: valid **E.164**, stored/compared normalized; unique.
- **Email**: valid format, max 254, **unique (case-insensitive)** via a normalized column.
- **Address**: governorate + city required and must exist; **city must belong to the selected
  governorate**; street ≤ 200; building/flat allow `12A`, `10/2` style values; 1–5 addresses,
  exactly one primary (auto-primary when only one).

## Testing

| Suite | Location | Run |
| --- | --- | --- |
| Domain unit (52) | `backend/tests/Registration.Domain.UnitTests` | `dotnet test` |
| Application unit (19) | `backend/tests/Registration.Application.UnitTests` | mapping validity, validators, handlers, behaviors |
| Integration | `backend/tests/Registration.IntegrationTests` | WebApplicationFactory + Testcontainers SQL 2019 |
| Frontend unit (21) | `frontend/src/**/*.test.ts(x)` | `npm test` |
| E2E | `frontend/e2e` | `npm run e2e` (happy path + validation failure) |

Backend: `cd backend && dotnet test`. The integration tests need a working Docker host (Testcontainers);
set `INTEGRATION_TEST_DB` to target an existing SQL Server where Docker-in-test is restricted.

## Bonus features (all implemented)

- ✅ **RabbitMQ + MassTransit** for async post-registration processing (welcome email/SMS, audit event).
- ✅ **Transactional Outbox** (MassTransit EF Core outbox) — the create transaction never depends on the broker.
- ✅ **Docker Compose** for API, SQL Server 2019, RabbitMQ, and the frontend.
- ✅ **Structured logging** (Serilog) with **correlation id** and request-duration logging (no PII logged).
- ✅ **Health checks** at `/health` for SQL Server and RabbitMQ (MassTransit bus).
- ✅ **Integration tests** (WebApplicationFactory + Testcontainers).
- ✅ **Frontend E2E** (Playwright) for the happy and validation-failure paths.
- ✅ **Pagination/search** endpoint for registrations.

## Technical decisions

- **.NET 8 (LTS)** for long-term support and mature SQL Server tooling.
- **Central Package Management** (`Directory.Packages.props`) keeps versions consistent across projects.
- **`IApplicationDbContext`** (the reference Clean Architecture approach) exposes `DbSet<T>` so query
  handlers compose EF queries directly; the Application owns the persistence contract, Persistence implements it.
- **Value objects mapped as EF owned types** with explicit `.Property(...)` declarations (required for
  EF constructor binding of get-only properties) and unique indexes on the normalized email/mobile columns.
- **Manual inbound mapping** (request → domain) because building value objects requires their factory
  methods and the injected submission date; **AutoMapper** is used for domain → DTO and its profile is
  unit-tested with `AssertConfigurationIsValid`.
- **AutoMapper 13.0.1** is pinned: the patched line for advisory `GHSA-rvv3-g6hj-g44x` (an
  uncontrolled-recursion DoS) is 15.1.1+, which requires a **commercial license key**. The advisory is
  not reachable here (all DTO↔entity maps are flat/non-recursive), so that single advisory is suppressed
  while NuGet auditing stays enabled for everything else.

## Repository layout

```
registration-module/
├─ backend/          # .NET 8 solution (Registration.sln)
│  ├─ src/           # Domain, Application, Persistence, Infrastructure, Api
│  └─ tests/         # Domain unit, Application unit, Integration tests
├─ frontend/         # React + TypeScript (Vite) app
├─ docker/           # Dockerfiles, nginx config, docker-compose.yml
└─ docs/             # Additional documentation
```

## License

For evaluation purposes as part of the 3S Group developer task.
