# AsyncLab.Ex02.TaskLifecycle

## Goal
Investigate async method lifecycle and Task behavior.

## Topics
- synchronous execution before first await
- Task lifecycle
- Task statuses
- continuation scheduling
- Task.WhenAll
- fire-and-forget behavior

## Key Observations
- async methods start executing immediately
- execution is synchronous until first await
- Task is not a thread
- await suspends execution instead of blocking thread
- unawaited tasks may never complete before process shutdown
- Task.WhenAll waits for completion of all tasks