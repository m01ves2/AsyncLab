# AsyncLab.Ex08 - Fake ASP.NET Pipeline

## Goal
Understand how ASP.NET Core middleware pipeline works with async/await and continuations.

## Key Observations
- Middleware execution flows downward through `await next()`
- After endpoint completion, execution resumes upward
- Request processing is not tied to a single thread
- Middleware behaves like nested async continuations