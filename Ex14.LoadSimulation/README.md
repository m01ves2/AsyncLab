# AsyncLab.Ex14.LoadSimulation

## Goal

Simulate backend server load and observe scalability differences between async and blocking request processing.

## Topics

- concurrent request processing
- ThreadPool behavior
- async vs blocking execution
- Task.Delay vs Thread.Sleep
- throughput under load
- ThreadPool pressure
- scalability observation

## Key Observations

- async requests scale efficiently under high load
- `Task.Delay` releases the thread back to the ThreadPool
- `Thread.Sleep` blocks the physical thread
- blocking operations reduce server throughput
- ThreadPool creates threads conservatively
- blocked threads can lead to ThreadPool starvation
- async pipelines allow many requests to wait concurrently
- a single blocking operation can degrade scalability
- modern web servers rely on async IO for scalability