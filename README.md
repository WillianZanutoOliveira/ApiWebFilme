<div align="center">

[🇺🇸 English](README.en.md)

# Golden Raspberry Awards API

### .NET 10 · ASP.NET Core · EF Core · NUnit · Docker · CI/CD

[![CI](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml/badge.svg)](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-SQLite-512BD4)
![Tests](https://img.shields.io/badge/Tests-Unit%20%2B%20Integration-22C55E)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)
![ADRs](https://img.shields.io/badge/Architecture-ADRs-7C3AED)

</div>

API de portfólio modernizada, construída com **C# e ASP.NET Core** para analisar dados do Golden Raspberry Awards, com regras de negócio isoladas, testes automatizados, Docker e decisões arquiteturais documentadas.

**Links rápidos:** [Arquitetura](docs/architecture.md) · [ADRs](docs/adr) · [CI](https://github.com/WillianZanutoOliveira/ApiWebFilme/actions/workflows/ci.yml)

A API identifica:

- o produtor com o **maior intervalo** entre premiações consecutivas;
- o produtor que recebeu **dois prêmios no menor intervalo**.

Este repositório é um dos meus projetos públicos de portfólio .NET e demonstra design de APIs, processamento de dados, persistência, testes de integração e CI/CD. O projeto foi modernizado de .NET 7 para **.NET 10** por meio de um Pull Request validado pelo CI.

## O que este projeto demonstra

- ASP.NET Core Web API
- endpoints REST
- Entity Framework Core
- abstração de repositórios
- ingestão de dados CSV
- Swagger / OpenAPI
- testes de integração
- regras de negócio de intervalos consecutivos cobertas por testes unitários
- Problem Details e health checks
- validação da imagem Docker no CI
- atualização de dependências com Dependabot
- conceitos de modelagem relacional e de dados

## Stack técnica

- **C#**
- **.NET 10**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQLite / banco em memória**
- **Swagger**
- **NUnit / testes unitários + integração**
- **Docker**
- **GitHub Actions**

> Este projeto foi desenvolvido originalmente em 2023. Ele é mantido como projeto público de portfólio e pode ser modernizado de forma incremental como parte do meu estudo contínuo de arquitetura e engenharia.

## Documentação de engenharia

- [Arquitetura](docs/architecture.md)
- [ADR-0001 — Modernização para .NET 10](docs/adr/0001-modernize-to-dotnet-10.md)
- [ADR-0002 — Cálculo de intervalos consecutivos entre premiações](docs/adr/0002-consecutive-award-intervals.md)
- [ADR-0003 — Seed no startup e GET somente leitura](docs/adr/0003-startup-data-seeding.md)

## Visão geral da arquitetura

```text
Cliente
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
          +--> dados de filmes/premiações
```

A solução separa as responsabilidades da API da lógica de acesso a dados por meio de repositórios e de um componente dedicado ao cálculo da regra de negócio. Os dados de referência são inicializados uma única vez no startup da aplicação, mantendo o endpoint GET público sem efeitos colaterais.

## Executando localmente

### Requisitos

- .NET 10 SDK

Clone o repositório:

```bash
git clone https://github.com/WillianZanutoOliveira/ApiWebFilme.git
cd ApiWebFilme
```

Restaure as dependências e execute:

```bash
dotnet restore
dotnet run --project ApiWebFilme/ApiWebFilme.csproj
```

Em ambiente de desenvolvimento, utilize o Swagger/OpenAPI para explorar os endpoints disponíveis.

## Docker

Construa a imagem:

```bash
docker build -t golden-raspberry-api .
```

Execute o container:

```bash
docker run --rm -p 8080:8080 golden-raspberry-api
```

O pipeline de CI também constrói a imagem do container, garantindo que o Dockerfile permaneça executável.

Endpoint de saúde:

```text
GET /health
```

## Testes

Execute os testes automatizados com:

```bash
dotnet test
```

O projeto de testes valida a API HTTP e a regra de negócio dos intervalos entre premiações. O calculador avalia explicitamente **vitórias consecutivas** de cada produtor, em vez de comparar apenas a primeira e a última vitória.

## Notas de engenharia

Este repositório representa uma etapa anterior da minha trajetória com .NET. Meu foco profissional atual inclui **.NET moderno, APIs, integrações, mensageria, CI/CD, observabilidade, cloud e arquitetura de software**.

Para cases atuais de arquitetura e meu posicionamento profissional, acesse meu perfil:
- https://github.com/WillianZanutoOliveira
