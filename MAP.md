AsyncLab Map
🧱 Блок 1 — фундамент
Цель: перестать путать Thread / Task / async

Ex01 — Thread vs ThreadPool
прямые Thread
ThreadPool поведение
race conditions
Ex02 — Task basics
Task.Run
Task lifecycle
WaitingForActivation
WhenAll
Ex03 — Continuation Flow
async method splitting
multiple await points
interleaving execution
Ex04 — Blocking vs Async
.Result
.Wait()
sync-over-async
thread blocking
Ex05 — CPU vs Async vs Parallelism
Task.Run vs sequential CPU
CPU-bound scaling
async does NOT help CPU work



🧠 Блок 2 — “опасная зона”

Цель: понять, где async ломается

Ex06 — ThreadPool starvation simulation
100+ blocking calls
why ASP.NET dies under .Result
Ex07 — Deadlock intuition
sync context
await + .Result deadlock patterns
Ex08 — Async in ASP.NET pipeline
request thread vs worker thread
middleware continuation flow deeper
Ex09 — Exception flow in Tasks
try/catch in async
Task exceptions wrapping



⚙️ Блок 3 — orchestration

Цель: научиться управлять задачами

Ex10 — Task combinators
WhenAll / WhenAny
race conditions in tasks
Ex11 — CancellationToken
cooperative cancellation
propagation of cancellation
Ex12 — Timeout patterns
Task.WhenAny + delay
proper timeout handling



🚀 Блок 4 — backend reality

Цель: думать как ASP.NET runtime

Ex13 — ASP.NET request lifecycle simulation
fake middleware pipeline
async flow in requests
Ex14 — Load simulation
many requests
thread pool pressure
scalability observation
Ex15 — Final synthesis
mixed CPU + IO system
design decision exercise
“when to use what”