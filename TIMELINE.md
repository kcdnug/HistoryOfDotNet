# Timeline and Theme Map

| Era | Framework / Runtime | C# | Main idea | Code we stopped writing |
|---|---:|---:|---|---|
| Foundation | .NET Framework 1.0/1.1 | 1.0/1.2 | Managed code becomes normal | COM plumbing, manual memory assumptions |
| Type safety | .NET Framework 2.0 | 2.0 | Generics, iterators, nullable value types | `ArrayList`, casts, custom enumerators |
| Query era | .NET Framework 3.0/3.5 | 3.0 | LINQ, lambdas, extension methods | many manual loops and temporary collections |
| Dynamic/interoperability | .NET Framework 4 | 4.0 | TPL, dynamic, optional/named args | some COM reflection and delegate ceremony |
| Async era | .NET Framework 4.5 | 5.0 | async/await | callbacks, Begin/End, tangled continuations |
| Compiler platform | .NET Framework 4.6+ | 6.0 | Roslyn, analyzers, code style | some external tooling assumptions |
| Cross-platform | .NET Core 1–3.1 | 6–8 | open source, CLI, side-by-side, ASP.NET Core | machine-wide framework assumptions, System.Web defaults |
| Unified .NET | .NET 5 | 9.0 | one platform, records | boilerplate DTO equality and mutable-only models |
| Ceremony removal | .NET 6 | 10.0 | minimal APIs, global usings, file-scoped namespaces | Startup ceremony and namespace noise |
| Expressive modeling | .NET 7 | 11.0 | required members, generic math, raw strings | some invalid object initialization and awkward literals |
| Compile-time shift | .NET 8 | 12.0 | Native AOT, source generators mainstream, collection expressions | some reflection-heavy runtime work |
| Refinement | .NET 9 | 13.0 | params collections, lock object, escape improvements | more incidental boilerplate |
| Maturity | .NET 10 | 14.0 | extension members, field keyword, partial events/constructors | patterns that required awkward helper APIs |
| Future-facing | .NET 11 preview | 15.0 | union types, runtime async | hand-rolled result types and non-exhaustive state handling |
