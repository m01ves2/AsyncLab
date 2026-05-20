Goal:
Investigate difference between blocking and non-blocking waiting.

Experiments:
- Thread.Sleep
- Task.Delay
- Sequential awaits
- Task.WhenAll

Observations:
- async does not create a new thread automatically
- await releases thread during I/O wait
- WhenAll enables concurrency