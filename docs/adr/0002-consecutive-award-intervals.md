# ADR-0002: Calculate intervals between consecutive producer wins

- **Status:** Accepted
- **Date:** 2026-10-03

## Context

The award API must identify producers with the minimum and maximum interval between consecutive wins.

An implementation that compares only the earliest and latest win for a producer is insufficient when that producer has more than two wins.

Example:

```text
1990 -> 1991 -> 2000
```

The relevant intervals are `1` and `9`, not only `10`.

## Decision

Extract the rule into a pure `AwardIntervalCalculator`.

The repository is responsible for retrieving winning producer/year data. The calculator is responsible for:

- grouping by producer;
- removing duplicate years;
- ordering wins;
- generating adjacent pairs;
- calculating intervals;
- finding global minimum and maximum intervals.

## Why separate the calculator

The rule becomes:

- independent of EF Core;
- easier to reason about;
- directly unit testable;
- reusable;
- safer to evolve without HTTP or persistence dependencies.

## Edge cases

If no producer has two distinct winning years, the result contains empty `Min` and `Max` collections.

This avoids a runtime failure from calling `Min()` or `Max()` on an empty sequence.

## Consequences

### Positive

- more accurate business behavior;
- lower coupling;
- clearer responsibility boundaries;
- stronger automated test coverage.

### Trade-off

- one additional application service/type is introduced in a small project, but the separation is justified by the business rule.
