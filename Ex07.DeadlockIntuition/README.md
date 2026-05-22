# AsyncLab.Ex07 - Deadlock Intuition

## Goal
Understand how blocking calls (`.Result`, `.Wait()`) can lead to deadlocks when combined with `await` and context capture.

## Key Idea
Deadlock occurs when a task continuation tries to resume on a thread that is blocked waiting for that same task to complete.