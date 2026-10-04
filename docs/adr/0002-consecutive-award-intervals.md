[🇺🇸 English](0002-consecutive-award-intervals.en.md)

# ADR-0002: Cálculo dos intervalos entre vitórias consecutivas de produtores

- **Status:** Aceito
- **Data:** 2026-10-03

## Contexto

A API precisa identificar os produtores com o menor e o maior intervalo entre vitórias consecutivas.

Uma implementação que compara apenas a primeira e a última vitória de um produtor é insuficiente quando ele possui mais de duas vitórias.

Exemplo:

```text
1990 -> 1991 -> 2000
```

Os intervalos relevantes são `1` e `9`, e não apenas `10`.

## Decisão

Extrair a regra para um `AwardIntervalCalculator` puro.

O repositório é responsável por recuperar os dados de produtor/ano das vitórias. O calculator é responsável por:

- agrupar por produtor;
- remover anos duplicados;
- ordenar as vitórias;
- gerar pares adjacentes;
- calcular os intervalos;
- encontrar os menores e maiores intervalos globais.

## Por que separar o calculator

A regra passa a ser:

- independente do EF Core;
- mais fácil de compreender;
- diretamente testável por testes unitários;
- reutilizável;
- mais segura para evoluir sem depender de HTTP ou persistência.

## Edge cases

Se nenhum produtor tiver dois anos distintos de vitória, o resultado contém coleções `Min` e `Max` vazias.

Isso evita falha em runtime ao chamar `Min()` ou `Max()` sobre uma sequência vazia.

## Consequências

### Positivas

- comportamento de negócio mais correto;
- menor acoplamento;
- limites de responsabilidade mais claros;
- cobertura de testes automatizados mais forte.

### Trade-off

- um tipo/serviço adicional de aplicação é introduzido em um projeto pequeno, mas a separação é justificada pela regra de negócio.
