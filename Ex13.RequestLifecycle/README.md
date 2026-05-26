# AsyncLab.Ex13.RequestLifecycle

## Goal

Simulate ASP.NET Core request lifecycle and middleware pipeline behavior.

## Topics

- middleware pipeline
- RequestDelegate chaining
- async request flow
- middleware BEFORE/AFTER execution
- request context propagation
- concurrent request processing
- Task.WhenAll orchestration

## Key Observations

- middleware controls execution flow through `next`
- request object flows through the entire pipeline
- `await next()` creates BEFORE/AFTER behavior
- middleware can execute logic before and after the next step
- multiple requests execute concurrently
- async requests interleave during execution
- pipeline execution is based on delegates and continuations
- ASP.NET Core middleware pipeline follows the same execution model