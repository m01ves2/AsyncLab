# AsyncLab.Ex12.TimeoutPatterns

## Goal

Understand how timeout behavior is implemented in .NET using Task race conditions rather than built-in task timeouts.

## Topics

- Task.WhenAny for timeout orchestration
- race condition between work and timeout tasks
- Task.Delay as timeout mechanism
- CancellationTokenSource.Cancel for cooperative cancellation
- difference between stopping execution and stopping observation
- WhenAll as aggregation of multiple tasks
- task continuation after timeout event

## Key Observations

- timeout in .NET is implemented as a race between tasks, not a built-in feature
- Task.WhenAny returns the first completed task (work or timeout)
- CancellationToken does not forcibly stop running tasks
- tasks that finish earlier than timeout still complete normally
- timeout only stops waiting for results, not task execution
- Task.WhenAll does not provide atomic cancellation behavior