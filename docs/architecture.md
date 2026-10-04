[🇺🇸 English](architecture.en.md)

# Arquitetura

## Visão geral

A Golden Raspberry Awards API é intencionalmente compacta, mas o projeto separa a orquestração HTTP, a persistência e uma regra de negócio testável para os intervalos entre premiações.

```mermaid
flowchart LR
    Client[Cliente HTTP] --> Controller[FilmesController]
    Controller --> Movies[IFilmesRepository]
    Controller --> Awards[IObterPremiosRepository]

    Awards --> EF[Entity Framework Core]
    Movies --> EF
    EF --> SQLite[(SQLite)]

    Awards --> Calc[AwardIntervalCalculator]
    CSV[Dataset CSV] --> Seeder[DatabaseSeeder]
    Seeder --> EF

    Health[/health] --> App[Saúde da aplicação]
    CI[GitHub Actions] --> Tests[Testes NUnit]
    CI --> Docker[Build Docker]
```

## Camada de API

O `FilmesController` expõe o endpoint de premiações e coordena a consulta dos dados.

O suporte a Problem Details do ASP.NET Core fornece respostas de erro consistentes, enquanto um endpoint leve `/health` atende verificações operacionais de liveness.

## Persistência

Entity Framework Core com SQLite mantém o projeto autocontido para execução local e CI.

Abstrações de repositório isolam o comportamento de acesso a dados da superfície HTTP.

## Regra de intervalo entre premiações

A implementação original comparava a primeira e a última vitória de cada produtor.

Essa abordagem pode ser incorreta quando um produtor possui três ou mais vitórias, pois a especificação trata dos intervalos entre **premiações consecutivas**.

O design atual consulta as entradas produtor/ano vencedoras e delega a regra ao `AwardIntervalCalculator`.

Para cada produtor, o calculator:

1. remove anos de vitória duplicados;
2. ordena os anos cronologicamente;
3. cria pares adjacentes;
4. calcula o intervalo de cada par consecutivo;
5. retorna todos os menores e maiores intervalos globais.

Se nenhum produtor possuir pelo menos dois anos de vitória, o calculator retorna coleções vazias em vez de lançar uma exceção.

A regra é coberta por testes NUnit dedicados.

## Estratégia de testes

A suíte inclui:

- comportamento HTTP end-to-end usando `WebApplicationFactory<Program>`;
- testes puros da regra de negócio do calculator;
- cobertura de edge cases para produtores com menos de duas vitórias;
- tratamento de anos duplicados.

## CI e entrega

O GitHub Actions executa:

1. restore de dependências;
2. build em Release;
3. testes unitários e de integração;
4. coleta de cobertura XPlat;
5. upload do artefato de cobertura;
6. build da imagem Docker.

O Dependabot está configurado para dependências NuGet e GitHub Actions.

## Container

O projeto utiliza um Dockerfile multi-stage em .NET 10.

A imagem final contém apenas o runtime do ASP.NET Core, a aplicação publicada e o dataset de portfólio necessário para a API.

## Modernização

O projeto foi desenvolvido originalmente em .NET 7 e posteriormente modernizado para .NET 10.

O hardening adicional separou o cálculo central de negócio, ampliou os testes e adicionou validação do container.

Consulte:

- [ADR-0001 — Modernização para .NET 10](adr/0001-modernize-to-dotnet-10.md)
- [ADR-0002 — Intervalos consecutivos entre premiações](adr/0002-consecutive-award-intervals.md)

## Ciclo de vida dos dados no startup

O dataset de premiações é tratado como dado de referência e carregado por um `DatabaseSeeder` dedicado durante o startup da aplicação.

O seeder carrega o CSV apenas quando o banco ainda não contém registros de filmes.

Isso mantém `GET /v1/api/filmes/premios` somente leitura e remove operações destrutivas de persistência do caminho da requisição.

A criação do banco é executada explicitamente no fluxo de composição do startup, em vez de ocorrer dentro do construtor do DbContext.

Consulte [ADR-0003](adr/0003-startup-data-seeding.md).
