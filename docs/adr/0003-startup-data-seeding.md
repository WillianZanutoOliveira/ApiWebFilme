# ADR-0003: Seed reference data at startup and keep GET requests read-only

- **Status:** Accepted
- **Date:** 2026-10-03

## Context

The original award endpoint rebuilt the local database on every `GET /v1/api/filmes/premios` request.

That meant a read operation performed destructive writes before returning data:

1. delete all movie records;
2. read the CSV dataset;
3. repopulate the database;
4. calculate the response.

This creates unnecessary work and makes a GET request non-read-only, which is undesirable for concurrency, observability and API semantics.

The DbContext constructor also called `EnsureCreated()`, introducing an infrastructure side effect during object construction.

## Decision

Move database initialization and reference-data seeding to application startup.

A dedicated `DatabaseSeeder`:

- checks whether movie data already exists;
- locates the reference CSV dataset;
- fails fast if the required dataset is missing;
- loads the data only when the database is empty.

The controller now performs only the query required to return award results.

Database creation is also moved out of the DbContext constructor and into the explicit startup composition flow.

## Consequences

### Positive

- GET requests no longer mutate persistence state;
- repeated requests avoid CSV parsing and full database rewrites;
- clearer separation between startup/infrastructure and HTTP query behavior;
- lower coupling in the controller;
- safer behavior under concurrent requests;
- easier operational reasoning.

### Trade-off

The current portfolio application seeds synchronously during startup. For a larger production dataset, this could be moved to a migration/deployment process or dedicated initialization job.
