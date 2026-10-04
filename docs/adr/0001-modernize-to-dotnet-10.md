# ADR-0001: Modernize the API to .NET 10

- **Status:** Accepted
- **Date:** 2026-10-03

## Context

The project was originally implemented with .NET 7. As a public portfolio project, keeping an end-of-life runtime would not accurately represent current .NET engineering practices.

## Decision

Upgrade the API and integration-test projects to **.NET 10** and align the main Microsoft packages with the .NET 10 release line.

The modernization includes:

- `net10.0` target framework;
- Entity Framework Core 10;
- ASP.NET Core testing packages 10;
- current Swagger tooling;
- current test SDK and coverage collector;
- GitHub Actions validation using .NET 10.

## Validation strategy

The change was developed on a dedicated branch and submitted through a pull request.

The PR was merged only after the GitHub Actions pipeline completed successfully.

## Consequences

### Positive

- supported/current runtime;
- portfolio reflects modern .NET development;
- reproducible automated validation;
- easier future package maintenance.

### Trade-offs

- the project remains intentionally small and does not introduce architectural complexity only for demonstration purposes;
- SQLite is retained to preserve low-friction local execution.

## Follow-up

Future improvements can focus on:

- stronger domain/application boundaries;
- additional negative/edge-case tests;
- structured error handling;
- containerized execution;
- richer observability.
