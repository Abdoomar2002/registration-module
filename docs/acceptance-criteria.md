# Acceptance criteria — where each is satisfied

| # | Acceptance criterion | Where |
| --- | --- | --- |
| 1 | Submit valid personal data + ≥1 valid address → created registration id | `CreateRegistrationCommandHandler`; `RegistrationsController.Create` returns 201 + `Location` |
| 2 | Invalid fields show clear frontend messages **and** are rejected by the backend | Zod schema (`registrationSchema.ts`) + `CreateRegistrationCommandValidator` (mirrored rules) |
| 3 | Duplicate Email or Mobile → **409 Conflict** | `EnsureUniqueAsync` + `DuplicateRegistrationException` → `GlobalExceptionHandler` (409); DB unique indexes as a race-safe backstop |
| 4 | City cannot be submitted under a different Governorate | `CreateAddressRequestValidator` async rule (city belongs to governorate) → 400 |
| 5 | Data saved correctly in SQL Server using EF Core migrations | `ApplicationDbContext` + `InitialCreate` migration; auto-applied on startup |
| 6 | Swagger documents all endpoints with example request/response | `AddSwaggerGen` + XML comments + `Swashbuckle.Filters` examples (`/swagger`) |
| 7 | Unit tests cover validation, domain/application behavior, command handlers, mapping config | Domain (52) + Application (19) tests, incl. `MappingProfileTests.AssertConfigurationIsValid` |
| 8 | Integration tests cover create API, lookup filtering, persistence, duplicate conflict, invalid gov/city | `RegistrationsEndpointsTests`, `LookupsEndpointsTests` (WebApplicationFactory + Testcontainers) |
| 9 | Structured logging for troubleshooting (correlation id, validation failures, duplicates, persistence, duration) **without PII** | Serilog + `CorrelationIdMiddleware` + `LoggingBehavior` (name + duration only) + `UseSerilogRequestLogging` |
| 10 | Clean architecture dependency direction (Domain independent) | Project references; Domain has zero package/project dependencies |

## Functional requirements

- Open form + enter personal details — frontend `RegistrationForm`.
- Add ≥1 address, optionally multiple (max 5) — `useFieldArray` + add/remove controls.
- Governorate/City lookups; City filtered by Governorate — `GetGovernoratesQuery` / `GetCitiesQuery`; dependent dropdown in `AddressForm`.
- Submit only when client + server validations pass — submit disabled while invalid/submitting; server re-validates.
- API stores data and returns the created id — 201 + `{ id }`.
- Prevents duplicate Email/Mobile — 409.
- Swagger exposes the contract + examples.
