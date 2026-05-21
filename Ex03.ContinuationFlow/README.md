# AsyncLab.Ex03.ContinuationFlow

## Goal
Investigate async continuation flow and execution interleaving.

## Topics
- multiple await points
- continuation scheduling
- execution interleaving
- Task.WhenAll

## Key Observations
- async methods execute synchronously until the first await
- async execution is split into continuation segments
- continuations may resume on different ThreadPool threads
- continuation execution order is nondeterministic
- the same Task instance survives across multiple await points
- console output may interleave between continuations