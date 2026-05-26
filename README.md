# AsyncLab

Educational project focused on understanding async/await, task orchestration, and backend execution flow in .NET.

## Goal

Build a simplified backend pipeline to understand how modern web servers handle:
- async IO operations
- CPU-bound work
- task coordination
- cancellation and timeouts
- request/response lifecycle

## What is implemented

### Request pipeline
- Logging middleware
- Authentication middleware
- Sequential request processing model

### Endpoint logic
- Parallel IO operations (`Task.WhenAll`)
  - Load user profile
  - Load user orders
- CPU-bound processing (statistics calculation)
- External service call simulation (recommendations)

### Reliability patterns
- Timeout handling using `Task.WhenAny`
- Cooperative cancellation with `CancellationToken`
- Graceful degradation (partial failure of recommendations does not break response)

### Response layer
- Aggregation of multiple data sources into a single response model
- JSON serialization simulation

## Key concepts practiced

- async / await fundamentals
- Task composition
- concurrency vs parallelism
- IO-bound vs CPU-bound work
- cancellation tokens
- basic backend pipeline architecture

## Output

Simulated dashboard response containing:
- Profile data
- Orders
- Statistics
- Recommendations (optional, timeout-protected)

## Notes

This project intentionally avoids frameworks (ASP.NET Core, EF Core) to focus on core runtime behavior and task orchestration.