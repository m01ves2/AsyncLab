# AsyncLab.Ex04.BlockingVsAsync

## Goal
Compare blocking async calls with proper asynchronous execution.

## Topics
- async vs blocking waits
- Task.Result
- Task.WhenAll
- ThreadPool continuations
- sync-over-async
- scalability implications

## Key Observations
- .Result blocks the calling thread until Task completion
- blocking async calls eliminate async scalability benefits
- await suspends execution without blocking the thread
- Task.WhenAll allows concurrent asynchronous execution
- async continuations may run on different ThreadPool threads
- sync-over-async patterns increase ThreadPool pressure
- blocking threads during I/O waiting harms backend scalability