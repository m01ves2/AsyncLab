# AsyncLab.Ex10.TaskCombinators

## Goal

Understand how Task.WhenAny orchestrates multiple asynchronous operations and how tasks behave after a winner is selected.

## Topics

- Task.WhenAny behavior and return type (Task<Task<T>>)
- task orchestration vs task cancellation
- task state lifecycle (WaitingForActivation, RanToCompletion)
- race conditions between tasks
- continuation after awaiting nested tasks
- Task.Delay as non-blocking timer-based async operation

## Key Observations

- WhenAny does not cancel or affect other running tasks
- returned value is the first completed Task, not its result
- remaining tasks continue execution independently
- awaiting the winner unwraps Task<Task<T>> → Task<T> → T
- Task.Delay does not occupy a thread and stays in waiting state
- task states may show WaitingForActivation even when logically "in progress"
- completion order is nondeterministic and depends on timing