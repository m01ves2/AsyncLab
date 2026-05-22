# AsyncLab.Ex05.CpuBoundVsAsync

## Goal
Compare CPU-bound parallel execution with asynchronous I/O-style execution.

## Topics
- CPU-bound work
- Task.Run
- ThreadPool execution
- fake async methods
- async vs parallelism
- CPU utilization

## Key Observations
- async does not automatically create parallel execution
- CPU-bound work requires active threads during the entire computation
- Task.Run schedules CPU work on ThreadPool threads
- parallel CPU execution can utilize multiple CPU cores
- async methods without await execute synchronously
- the async keyword alone does not make code asynchronous
- I/O-bound async and CPU-bound parallelism solve different problems