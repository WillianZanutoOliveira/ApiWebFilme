[🇧🇷 Português](architecture.md)

# Architecture

## Overview

The Golden Raspberry Awards API is intentionally compact, but the project separates HTTP orchestration, persistence and a testable business rule for award intervals.

```mermaid
flowchart LR
    Client[HTTP Client] --> Controller[FilmesController]
    Controller --> Movies[IFilmesRepository]
    Controller --> Awards[IObterPremiosRepository]

    Awards --> EF[Entity Framework Core]
    Movies --> EF
    EF --> SQLite[(SQLite)]

    Awards --> Calc[AwardIntervalCalculator]
    CSV[CSV dataset] --> Seeder[DatabaseSeeder]
    Seeder --> EF

    Health[/health] --> App[Application Health]
    CI[GitHub Actions] --> Tests[NUnit tests]
    CI --> Docker[Docker build]
```

## API layer

`FilmesController` exposes the award endpoint and coordinates dataset initialization and query execution.

ASP.NET Core Problem Details support is enabled for consistent error responses and a lightweight `/health` endpoint supports operational liveness checks.

## Persistence

Entity Framework Core with SQLite keeps the project self-contained for local execution and CI.

Repository abstractions isolate data-access behavior from the HTTP surface.

## Award interval rule

The original implementation compared the earliest and latest win for each producer.

That approach can be incorrect when a producer has three or more wins because the specification concerns intervals between **consecutive awards**.

The current design queries winning producer/year entries and delegates the rule to `AwardIntervalCalculator`.

For each producer the calculator:

1. removes duplicate winning years;
2. orders winning years chronologically;
3. creates adjacent year pairs;
4. calculates the interval for each consecutive pair;
5. returns all global minimum and maximum intervals.

If no producer has at least two winning years, the calculator returns empty collections rather than throwing an exception.

This rule is covered by dedicated NUnit tests.

## Testing strategy

The test suite includes:

- end-to-end HTTP behavior through `WebApplicationFactory<Program>`;
- pure business-rule tests for the interval calculator;
- edge-case coverage for producers with fewer than two wins;
- duplicate-year handling.

## CI and delivery

GitHub Actions performs:

1. dependency restore;
2. Release build;
3. unit and integration tests;
4. XPlat code-coverage collection;
5. coverage artifact upload;
6. Docker image build.

Dependabot is configured for NuGet dependencies and GitHub Actions.

## Container

The project uses a multi-stage .NET 10 Dockerfile.

The final image contains only the ASP.NET Core runtime, published application and the portfolio dataset required by the API.

## Modernization

The project was originally developed on .NET 7 and later modernized to .NET 10.

Further hardening separated the core business calculation, expanded tests and added container validation.

See:

- [ADR-0001 — Modernize to .NET 10](adr/0001-modernize-to-dotnet-10.en.md)
- [ADR-0002 — Consecutive award intervals](adr/0002-consecutive-award-intervals.en.md)

## Startup data lifecycle

The award dataset is reference data and is loaded by a dedicated `DatabaseSeeder` during application startup.

The seeder only loads the CSV when the database does not already contain movie records.

This keeps `GET /v1/api/filmes/premios` read-only and removes destructive persistence operations from the request path.

Database creation is performed explicitly in the startup composition flow instead of inside the DbContext constructor.

See [ADR-0003](adr/0003-startup-data-seeding.en.md).
