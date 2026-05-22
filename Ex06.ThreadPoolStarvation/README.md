# AsyncLab.Ex06 - ThreadPool Starvation

## Goal
Understand how ThreadPool saturation affects application performance and scalability.

## Experiments

### Experiment 1 - Async execution (GOOD server)
- 100 concurrent tasks executed via Task.WhenAll
- Each task uses `await Task.Delay(2000)`
- No thread blocking occurs

**Result:**
- ~2 seconds total execution time
- High concurrency with minimal thread usage
- Threads are released back to ThreadPool during awaiting

---

### Experiment 2 - Blocking execution (BAD server)
- 100 tasks executed via Task.Run + `.Wait()`/`.Result`
- Each task blocks a ThreadPool thread for the full duration

**Result:**
- ~4–5 seconds execution time
- ThreadPool threads become blocked
- Continuations compete for available threads
- Reduced throughput due to thread starvation

---

## Key Observations

- `async/await` does NOT block threads during I/O waiting
- Blocking calls (`.Wait()`, `.Result`) consume ThreadPool threads
- ThreadPool is a limited resource and scales gradually
- Starvation occurs when all threads are blocked waiting for work

---

## Key Takeaway

Scalability in .NET backend systems depends on avoiding thread blocking, not on increasing thread count.