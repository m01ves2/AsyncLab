# AsyncLab.Ex09.ExceptionFlow

## Goal

Investigate exception flow in async Tasks.

## Topics

- exceptions inside async methods
- exception propagation through await
- Task faulted state
- Task.WhenAll exception aggregation
- AggregateException
- await vs Result exception behavior

## Key Observations

- exceptions inside async methods are stored inside Task
- await rethrows exception from faulted Task
- Task.WhenAll waits for all Tasks before throwing
- multiple Task failures are aggregated
- await unwraps a single exception
- AggregateException stores all inner exceptions
- Result blocks the current thread
- await does not block the thread