# Registration Module

A full-stack **user registration** module built for the 3S Group developer task.

- **Frontend:** React + TypeScript (Vite), React Hook Form + Zod, accessible reusable components.
- **Backend:** .NET 8 REST API using **Clean Architecture**, **CQRS** (MediatR), **FluentValidation**, **AutoMapper**, **EF Core / SQL Server 2019**.
- **Messaging (bonus):** RabbitMQ + MassTransit with the **Transactional Outbox** pattern for reliable post-registration processing.
- **Ops (bonus):** Docker Compose, Serilog structured logging with correlation IDs, health checks, unit + integration + E2E tests.

> **Status:** built incrementally via feature branches → pull requests. See the
> [open/merged PRs](https://github.com/Abdoomar2002/registration-module/pulls?q=is%3Apr) for the layer-by-layer history.

---

## Solution at a glance

A user fills a registration form (personal details + 1–5 addresses, exactly one primary).
The API validates the request on the server (mirroring client rules), persists it to SQL Server,
enforces uniqueness of email/mobile, and asynchronously publishes a `RegistrationCreated`
integration event through an outbox so a welcome email/SMS and audit record can be produced
without coupling them to the create transaction.

```
                 ┌──────────────┐      POST /api/registrations        ┌────────────────────┐
  React + TS  ───▶   REST API   │ ───────────────────────────────────▶  Application (CQRS) │
  (Vite)      ◀───   (.NET 8)   │   201 Created + Location header     │  + FluentValidation │
                 └──────┬───────┘                                     └─────────┬──────────┘
                        │                                                       │
                        │ EF Core                                               │ domain events
                        ▼                                                       ▼
                 ┌──────────────┐        outbox            ┌────────────┐   ┌────────────────┐
                 │ SQL Server   │ ───────────────────────▶ │  RabbitMQ  │──▶│ Welcome / Audit │
                 │   2019       │   (MassTransit outbox)    │ MassTransit│   │   consumers     │
                 └──────────────┘                          └────────────┘   └────────────────┘
```

## Architecture (Clean Architecture)

Dependencies point **inward** only. The Domain has no knowledge of EF Core, MediatR, AutoMapper,
or any framework.

| Project | Responsibility | May depend on |
| --- | --- | --- |
| `Registration.Domain` | Entities, value objects, domain events, invariants. | *(nothing)* |
| `Registration.Application` | CQRS commands/queries, MediatR handlers, DTOs, FluentValidation, AutoMapper profiles, interfaces, pipeline behaviors. | Domain |
| `Registration.Persistence` | EF Core `DbContext`, Fluent configs, migrations, seed data, interceptors (audit + outbox). | Application, Domain |
| `Registration.Infrastructure` | RabbitMQ/MassTransit, outbox delivery, email/SMS, DI registration for integrations. | Application, Domain, Persistence |
| `Registration.Api` | ASP.NET Core controllers, Swagger, ProblemDetails, correlation middleware, Serilog, health checks. Composition root. | Application, Infrastructure, Persistence |

Tests: `*.Domain.UnitTests`, `*.Application.UnitTests`, `*.IntegrationTests` (WebApplicationFactory + Testcontainers).

## Repository layout

```
registration-module/
├─ backend/          # .NET 8 solution (Registration.sln)
│  ├─ src/           # Domain, Application, Persistence, Infrastructure, Api
│  └─ tests/         # Domain unit, Application unit, Integration tests
├─ frontend/         # React + TypeScript (Vite) app
├─ docker/           # Dockerfiles + compose for full local stack
└─ docs/             # Additional documentation
```

## Getting started

> Full instructions land with the Docker Compose PR. In short:

```bash
# Everything (API + SQL Server 2019 + RabbitMQ + frontend) via Docker:
docker compose -f docker/docker-compose.yml up --build
```

Local dev without Docker requires the .NET 8 SDK, Node 20+, and a reachable SQL Server instance;
see `backend/` and `frontend/` for per-app instructions.

## Technical decisions

- **.NET 8 (LTS)** for long-term support and SQL Server tooling maturity.
- **Central Package Management** (`Directory.Packages.props`) keeps versions consistent across projects.
- **AutoMapper 13.0.1** is intentionally pinned. The patched line for advisory `GHSA-rvv3-g6hj-g44x`
  (an uncontrolled-recursion DoS) is 15.1.1+, which requires a **commercial license key**. The
  advisory is not reachable here because all DTO↔entity maps are flat and non-recursive, so the
  specific advisory is suppressed (and only that one) while NuGet auditing stays enabled for everything else.

## License

For evaluation purposes as part of the 3S Group developer task.
