# Golden Raspberry Awards API

[![CI](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml/badge.svg)](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml)

REST API built with **C# and ASP.NET Core** to analyze Golden Raspberry Awards data and identify:

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
- relational/data modeling concepts

## Tech stack

- **C#**
- **.NET 10**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQLite / in-memory database**
- **Swagger**
- **xUnit / integration testing**

> This project was originally developed in 2023. It is preserved as a public portfolio project and may be modernized incrementally as part of my continuous architecture and engineering studies.

## Engineering documentation

- [Architecture](docs/architecture.md)
- [ADR-0001 — Modernize to .NET 10](docs/adr/0001-modernize-to-dotnet-10.md)

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

The solution separates API concerns from data-access logic through repositories and uses automated tests to validate the expected API behavior.

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

## Tests

Run the automated tests with:

```bash
dotnet test
```

The test project validates the API behavior against the expected Golden Raspberry Awards rules.

## Engineering notes

This repository reflects an earlier stage of my .NET work. My current professional focus includes **modern .NET, APIs, integrations, messaging, CI/CD, observability, cloud and software architecture**.

For current architecture case studies and professional positioning, visit my profile:
- https://github.com/WillianZanutoOliveira
