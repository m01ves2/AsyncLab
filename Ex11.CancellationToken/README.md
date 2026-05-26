# AsyncLab.Ex11.CancellationToken

## Goal

Understand cooperative cancellation in .NET and how CancellationToken affects running asynchronous operations.

## Topics

- CancellationTokenSource lifecycle
- cooperative cancellation model
- Task.Delay with cancellation token
- OperationCanceledException propagation
- Task states (RanToCompletion, Canceled, Faulted)
- difference between cancellation and exception
- cancellation vs execution flow timing

## Key Observations

- CancellationToken does not stop tasks forcibly; it is a cooperative signal
- Task.Delay with a token completes as canceled when cancellation is triggered
- cancellation is observed at specific cancellation-aware points (e.g., await Task.Delay)
- OperationCanceledException is thrown when awaiting a canceled task
- most cancellation logic is handled by the awaited operation, not manual checks
- tasks may never reach subsequent code after an awaited cancellation point
- task status reflects final state (Canceled) rather than runtime “interruption”
- CancellationTokenSource should be disposed when its lifetime ends