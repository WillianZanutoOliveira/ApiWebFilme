<div align="center">

[🇧🇷 Português](README.md)

# Golden Raspberry Awards API

### .NET 10 · ASP.NET Core · EF Core · NUnit · Docker · CI/CD

[![CI](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml/badge.svg)](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-SQLite-512BD4)
![Tests](https://img.shields.io/badge/Tests-Unit%20%2B%20Integration-22C55E)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)
![ADRs](https://img.shields.io/badge/Architecture-ADRs-7C3AED)

</div>

Modernized portfolio API built with **C# and ASP.NET Core** to analyze Golden Raspberry Awards data, with isolated business rules, automated tests, Docker and documented architectural decisions.

**Quick links:** [Architecture](docs/architecture.md) · [ADRs](docs/adr) · [CI](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml)

The API identifies:

- the producer with the **longest interval** between consecutive awards;
- the producer who received **two awards in the shortest interval**.

This repository is one of my public .NET portfolio projects and demonstrates API design, data processing, persistence, integration testing and CI/CD. The project was modernized from .NET 7 to **.NET 10** through a CI-validated pull request.

## What this project demonstrates

- ASP.NET Core Web API
- REST endpoints
- Entity Framework Core
- repository abstraction
- CSV data ingestion
- Swagger / OpenAPI
- integration tests
- unit-tested business rules for consecutive award intervals
- Problem Details and health checks
- Docker image validation in CI
- dependency update automation with Dependabot
- relational/data modeling concepts

## Tech stack

- **C#**
- **.NET 10**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQLite / in-memory database**
- **Swagger**
- **NUnit / unit + integration testing**
- **Docker**
- **GitHub Actions**

> This project was originally developed in 2023. It is preserved as a public portfolio project and may be modernized incrementally as part of my continuous architecture and engineering studies.

## Engineering documentation

- [Architecture](docs/architecture.md)
- [ADR-0001 — Modernize to .NET 10](docs/adr/0001-modernize-to-dotnet-10.md)
- [ADR-0002 — Calculate consecutive award intervals](docs/adr/0002-consecutive-award-intervals.md)
- [ADR-0003 — Startup data seeding and read-only GET](docs/adr/0003-startup-data-seeding.md)

## Architecture overview

```text
Client
  |
  v
ASP.NET Core API
  |
  +--> Controllers
  |
  +--> Repositories
  |
  +--> EF Core
          |
          +--> award/movie data
```

The solution separates API concerns from data-access logic through repositories and a dedicated business-rule calculator. Reference data is initialized once at application startup, so the public GET endpoint remains read-only.

## Running locally

### Requirements

- .NET 10 SDK

Clone the repository:

```bash
git clone https://github.com/WillianZanutoOliveira/ApiWebFilme.git
cd ApiWebFilme
```

Restore and run:

```bash
dotnet restore
dotnet run --project ApiWebFilme/ApiWebFilme.csproj
```

When running in development, use Swagger/OpenAPI to explore the available endpoints.

## Docker

Build the production-style image:

```bash
docker build -t golden-raspberry-api .
```

Run the container:

```bash
docker run --rm -p 8080:8080 golden-raspberry-api
```

The CI pipeline also builds the container image, ensuring the Dockerfile stays executable.

Health endpoint:

```text
GET /health
```

## Tests

Run the automated tests with:

```bash
dotnet test
```

The test project validates the HTTP API and the award-interval business rule. The calculator explicitly evaluates **consecutive wins** for each producer rather than only comparing the first and last win.

## Engineering notes

This repository reflects an earlier stage of my .NET work. My current professional focus includes **modern .NET, APIs, integrations, messaging, CI/CD, observability, cloud and software architecture**.

For current architecture case studies and professional positioning, visit my profile:
- https://github.com/WillianZanutoOliveira
