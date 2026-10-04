[🇺🇸 English](0001-modernize-to-dotnet-10.en.md)

# ADR-0001: Modernização da API para .NET 10

- **Status:** Aceito
- **Data:** 2026-10-03

## Contexto

O projeto foi originalmente implementado com .NET 7. Como projeto público de portfólio, manter um runtime fora de suporte não representaria adequadamente as práticas atuais de engenharia .NET.

## Decisão

Atualizar os projetos da API e de testes de integração para **.NET 10** e alinhar os principais pacotes Microsoft com a linha de versões do .NET 10.

A modernização inclui:

- target framework `net10.0`;
- Entity Framework Core 10;
- pacotes de testes ASP.NET Core 10;
- tooling atual do Swagger;
- SDK de testes e coletor de cobertura atuais;
- validação no GitHub Actions usando .NET 10.

## Estratégia de validação

A alteração foi desenvolvida em uma branch dedicada e submetida por Pull Request.

O PR só foi integrado depois que o pipeline do GitHub Actions foi concluído com sucesso.

## Consequências

### Positivas

- runtime atual e suportado;
- o portfólio passa a refletir desenvolvimento .NET moderno;
- validação automatizada reproduzível;
- manutenção futura de pacotes mais simples.

### Trade-offs

- o projeto permanece intencionalmente pequeno e não adiciona complexidade arquitetural apenas para fins demonstrativos;
- SQLite é mantido para preservar uma execução local simples.

## Próximos passos

Melhorias futuras podem focar em:

- limites mais fortes entre domínio e aplicação;
- testes adicionais para cenários negativos e edge cases;
- tratamento estruturado de erros;
- execução containerizada;
- observabilidade mais rica.
