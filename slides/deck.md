---
marp: true
theme: code-history
paginate: true
footer: The Code We Don't Write Anymore
---

<!-- _class: title -->

# The Code We Don't Write Anymore

<div class="subtitle">How .NET Changed the Way We Think About Code</div>

<div class="meta">A chronological walk through .NET Framework, .NET Core, modern .NET, and C#</div>

<!--
Speaker notes:
- Purpose: Establish this as a story about changing defaults, not a release-notes tour.
- Emphasize: We are not here to dunk on old code. Old code solved real problems with the tools available at the time.
- Transition: Start with the question that drives the whole talk.
-->

---

# One question for every release

> What did this make normal?

<!--
Speaker notes:
- This is the thesis. Every release changes what is possible, but only some change what we actually write.
- Tell the audience this question will be repeated throughout the deck.
-->

---

# This is not a feature checklist

<div class="callout">It is a history of defaults.</div>

- What felt natural?
- What became embarrassing boilerplate?
- What did the compiler take off our plate?

<!--
Speaker notes:
- Set expectations: some releases will be compressed; some get more attention.
- The time allocation follows the impact on everyday code, not the size of the release.
-->

---

# Old code was not bad code

<div class="punchline">It was code written before the platform taught us a better default.</div>

<!--
Speaker notes:
- This helps avoid sounding dismissive.
- Use your own early .NET memory here if you want.
- Transition: Let's start where everything was explicit.
-->

---

<!-- _class: section -->

# Foundation

.NET Framework 1.0 / 1.1 + C# 1.x

<div class="big-number">2002</div>

<!--
Speaker notes:
- This section is about the baseline: managed code, the BCL, events, delegates, and the shape of early enterprise .NET.
-->

---

# .NET 1.x: everything was explicit

<div class="columns">
<div>

### The good parts

- Managed code
- Garbage collection
- Common type system
- Events and delegates
- Rich base class library

</div>
<div>

### The pain points

- `object` everywhere
- Manual casts
- XML-heavy configuration
- Reflection ceremony
- Early async patterns

</div>
</div>

<!--
Speaker notes:
- The point is not that .NET 1 was primitive; it was a huge leap.
- But it had not yet learned the pain points developers would experience at scale.
-->

---

# Before generics, collections forgot types

```csharp
ArrayList customers = new ArrayList();
customers.Add(new Customer("Ada"));
customers.Add(new Order(1234)); // also allowed

Customer customer = (Customer)customers[0];
```

<!--
Speaker notes:
- This is the “cast-and-pray” era.
- The collection couldn't communicate intent. The compiler couldn't help.
- Constraint: generics didn't exist in the runtime yet. The only type-safe alternative was hand-writing a collection class per type — `CustomerCollection : CollectionBase` with typed `Add`, `Item`, `Insert` — repeated for every domain type. Some shops generated them.
- So `ArrayList` wasn't laziness. It was the cheaper of two bad options.
- Transition: .NET 2 changes that.
-->

---

# What became normal?

<div class="punchline">The runtime gave us safety. The language had not yet given us intent.</div>

<!--
Speaker notes:
- Foundation era takeaway.
- Now move into the first major “we stopped writing that” moment.
-->

---

<!-- _class: section -->

# Type Safety Becomes Everyday

.NET Framework 2.0 + C# 2.0

<div class="big-number">2005</div>

<!--
Speaker notes:
- Generics are the star.
- Mention iterators and nullable value types, but spend the most time on generics.
-->

---

# Generics ended cast-and-pray

<div class="columns">
<div>
<span class="compare-label">Old default</span>

```csharp
ArrayList items = new ArrayList();
items.Add(new Customer("Ada"));

Customer customer =
    (Customer)items[0];
```
</div>
<div>
<span class="compare-label">New default</span>

```csharp
List<Customer> items = new();
items.Add(new Customer("Ada"));

Customer customer = items[0];
```
</div>
</div>

<!--
Speaker notes:
- This is the first big feature that changed daily habits.
- API signatures became more meaningful.
- The compiler became a collaborator instead of a bystander.
-->

---

# We stopped programming against `object`

<div class="callout"><code>List&lt;Customer&gt;</code> is not just safer than <code>ArrayList</code> — it communicates intent.</div>

<!--
Speaker notes:
- Talk about performance too: less boxing, fewer runtime casts.
- But keep the larger point: code now carried meaning.
-->

---

# Iterators deleted custom enumerators

<div class="columns">
<div>
<span class="compare-label">Old default</span>

```csharp
public IEnumerator GetEnumerator()
{
    return new CustomerEnumerator(this);
}
```
</div>
<div>
<span class="compare-label">New default</span>

```csharp
public IEnumerable<Customer> Active()
{
    foreach (var customer in customers)
        if (customer.IsActive)
            yield return customer;
}
```
</div>
</div>

<!--
Speaker notes:
- `yield return` is easy to underappreciate.
- It made lazy sequences simple before LINQ made them central.
-->

---

# Nullable value types named absence

```csharp
DateTime? shippedAt = order.ShippedAt;

if (shippedAt.HasValue)
{
    Console.WriteLine(shippedAt.Value);
}
```

<!--
Speaker notes:
- This is not nullable reference types yet.
- It is the beginning of representing absence in the type system.
- Later we revisit this with C# 8.
-->

---

# What became normal?

<div class="punchline">Types became a way to say what code means, not just what memory holds.</div>

<!--
Speaker notes:
- Transition to LINQ: once collections remembered their element types, the platform could teach us a new way to work with them.
-->

---

<!-- _class: section -->

# Thinking in Sequences

.NET Framework 3.0 / 3.5 + C# 3.0

<div class="big-number">2006</div>

<!--
Speaker notes:
- This section deserves time.
- LINQ is one of the most important mindset shifts in the talk.
-->

---

# LINQ was not one feature

<div class="timeline-grid">
<div class="tile"><div class="era">Lambdas</div><div class="what">functions inline</div></div>
<div class="tile"><div class="era">Extension methods</div><div class="what">queries as fluent APIs</div></div>
<div class="tile"><div class="era">Anonymous types</div><div class="what">temporary shape</div></div>
<div class="tile"><div class="era">Expression trees</div><div class="what">code as data</div></div>
</div>

<!--
Speaker notes:
- C# 3 was a coordinated release. Many features existed to make LINQ possible.
- Mention `var` as practical, not magical.
-->

---

# Before LINQ: loops first

```csharp
List<Customer> preferred = new();

foreach (Customer customer in customers)
{
    if (customer.TotalSpend >= 1000)
    {
        preferred.Add(customer);
    }
}
```

<!--
Speaker notes:
- This code is perfectly reasonable.
- But the loop mechanics dominate the business question.
-->

---

# After LINQ: intent first

```csharp
var preferred = customers
    .Where(c => c.TotalSpend >= 1000)
    .ToList();
```

<!--
Speaker notes:
- The question is now visible: preferred customers are customers with total spend >= 1000.
- Mechanics moved into reusable sequence operators.
-->

---

# We stopped asking “how do I loop?”

<div class="punchline">We started asking “what data do I want?”</div>

<!--
Speaker notes:
- This is the mindset shift.
- LINQ made data flow read left-to-right or clause-by-clause.
-->

---

# Query syntax made relationships readable

```csharp
var query =
    from customer in customers
    join order in orders on customer.Id equals order.CustomerId
    where order.Total > 500
    select new
    {
        customer.Name,
        order.Total
    };
```

<!--
Speaker notes:
- Don't debate query vs method syntax too long.
- Use this to show that LINQ also reached joins, projections, and providers.
-->

---

# Expression trees changed libraries

```csharp
Expression<Func<Customer, bool>> filter =
    customer => customer.IsActive;
```

<div class="callout">The same syntax could describe work to do now, or work another provider could translate later.</div>

<!--
Speaker notes:
- This is a hinge from language feature to ecosystem shift.
- ORMs, query providers, mocking libraries, validation libraries all benefit from code-as-data.
-->

---

# What became normal?

<div class="punchline">Collections became pipelines.</div>

<!--
Speaker notes:
- Transition: The next era makes asynchronous work composable too.
-->

---

<!-- _class: section -->

# Work Becomes Asynchronous

.NET Framework 4 / 4.5 + C# 4 / 5

<div class="big-number">2010</div>

<!--
Speaker notes:
- Mention .NET 4 as setup: TPL, tasks, dynamic, variance.
- The main event is async/await.
-->

---

# .NET 4 prepared the ground

- Task Parallel Library
- `Task` as a common abstraction
- `dynamic` for interop
- optional and named arguments
- variance improvements

<!--
Speaker notes:
- Keep this brief.
- TPL matters because async/await builds on Task.
-->

---

# Before async/await

```csharp
var request = WebRequest.Create(orderUrl);

request.BeginGetResponse(ar1 =>
{
    var order = ReadOrder(request.EndGetResponse(ar1));
    var next = WebRequest.Create(CustomerUrl(order));

    next.BeginGetResponse(ar2 =>
    {
        var customer = ReadCustomer(next.EndGetResponse(ar2));
        uiContext.Post(_ => UpdateUi(order, customer), null);
    }, null);
}, null);
```

<!--
Speaker notes:
- Two sequential calls: the second URL depends on the first result. That dependency is what forces the nesting.
- Constraint: without a compiler-generated state machine, control flow couldn't cross an asynchronous boundary. No `try`/`catch`, no `using`, no `foreach`, no `if` spanning the wait — any state had to be carried by hand into the callback.
- Ask where the error handling is. There isn't any, and you can't add it: a `try` around `BeginGetResponse` returns before the work happens, so it catches nothing. Each callback needed its own.
- Point at `uiContext.Post`: marshaling back to the UI thread was manual. `await` later did this for free.
- Callback pyramids weren't a style choice. A loop containing a wait was the thing you couldn't express — it needed recursion.
- `ReadOrder` / `ReadCustomer` hide the stream and `using` plumbing. The real code was worse than this.
-->

---

# After async/await: code kept its shape

```csharp
try
{
    Order order = Parse(await client.GetStringAsync(orderUrl));
    Customer customer = Parse(await client.GetStringAsync(CustomerUrl(order)));

    UpdateUi(order, customer);
}
catch (HttpRequestException ex)
{
    Log(ex);
}
```

<!--
Speaker notes:
- Same two calls, same dependency between them. The nesting is gone.
- One `try`/`catch` now covers both operations, because the compiler rewrote the method into a state machine.
- No manual marshaling back to the UI thread — the synchronization context is captured for you.
- The code reads like normal control flow. But the scalability and responsiveness model is different.
- Add a third step and the old version grows another nesting level; this one grows one line.
-->

---

# We stopped coordinating threads

<div class="punchline">We started coordinating work.</div>

<!--
Speaker notes:
- Important distinction: async is not “make it faster.”
- It is about not blocking while waiting.
-->

---

# Async changed API design

<div class="columns">
<div>

### Before

```csharp
Stream ReadOrderStream(int id);
void Save(Order order);
```

</div>
<div>

### After

```csharp
Task<Stream> ReadOrderStreamAsync(int id);
Task SaveAsync(Order order);
```

</div>
</div>

<!--
Speaker notes:
- Async didn't stay at the edges.
- Once one layer became async, the shape propagated through the architecture.
-->

---

# What became normal?

<div class="punchline">Every I/O path became suspicious until proven async.</div>

<!--
Speaker notes:
- Good transition to Roslyn and tooling: the compiler keeps taking on more responsibility.
-->

---

<!-- _class: section -->

# The Compiler Becomes a Platform

Roslyn + C# 6

<div class="big-number">2015</div>

<!--
Speaker notes:
- This section connects language polish to analyzers, refactorings, source generators, and later compile-time work.
-->

---

# C# 6 polished everyday code

<div class="columns">
<div>
<span class="compare-label">Old default</span>

```csharp
throw new ArgumentNullException("customer");

string message = string.Format(
    "Hello, {0}", customer.Name);
```
</div>
<div>
<span class="compare-label">New default</span>

```csharp
throw new ArgumentNullException(
    nameof(customer));

string message = $"Hello, {customer.Name}";
```
</div>
</div>

<!--
Speaker notes:
- This isn't a revolution like LINQ, but it matters.
- The language started sanding down common annoyances.
-->

---

# Roslyn changed what tools could know

<div class="callout">The compiler was no longer just a thing that produced assemblies.</div>

<!--
Speaker notes:
- Talk about analyzers, code fixes, refactorings.
- This is setup for source generators later.
-->

---

# What became normal?

<div class="punchline">Code style, correctness, and refactoring moved closer to the compiler.</div>

<!--
Speaker notes:
- Transition: while the compiler opened up, the runtime and platform opened up too.
-->

---

<!-- _class: section -->

# The Platform Reboots

.NET Core 1.0–3.1 + C# 7 / 8

<div class="big-number">2016</div>

<!--
Speaker notes:
- This section is less about a single language feature and more about platform assumptions.
-->

---

# .NET Core changed assumptions

- Cross-platform by default
- Open source development model
- CLI-first workflows
- NuGet-first dependencies
- Side-by-side runtimes
- ASP.NET Core instead of System.Web

<!--
Speaker notes:
- Many people in the room may have entered .NET during this era.
- For older .NET developers, this was a major identity shift.
- Constraint: Framework installed machine-wide, one version at a time, upgraded in place, on a cadence tied to Windows itself. `System.Web` was welded to IIS.
- The consequence that forced the rewrite: two apps on one machine could not target two versions. Side-by-side was architecturally impossible, and cloud deployment made that fatal.
- So this was a rebuild, not an extension. Mono and Xamarin had already proven a portable runtime was feasible.
-->

---

# We stopped assuming Windows

<div class="columns">
<div>
<span class="compare-label">Old assumption</span>

```csharp
var path =
    ConfigurationManager
        .AppSettings["ExportPath"];

Process.Start("notepad.exe", path);
```
</div>
<div>
<span class="compare-label">New assumption</span>

```csharp
var path = options.ExportPath;

await fileStore.SaveAsync(
    path, content);
```
</div>
</div>

<!--
Speaker notes:
- The sample is intentionally broad: config and OS assumptions changed.
- Modern code tends to abstract environment dependencies earlier.
-->

---

# C# 7 made shape more visible

```csharp
if (value is Customer customer)
{
    Console.WriteLine(customer.Name);
}

var (name, total) = GetCustomerSummary(id);
```

<!--
Speaker notes:
- Pattern matching begins here.
- Tuples make lightweight returns feel normal.
-->

---

# Before patterns: type tests were noisy

```csharp
if (shape is Circle)
{
    var circle = (Circle)shape;
    return Math.PI * circle.Radius * circle.Radius;
}
```

<!--
Speaker notes:
- Again, old code is not bad; it is simply mechanical.
- Constraint: the type test and the cast couldn't be a single expression, so the type had to be named twice and the runtime re-checked what the `if` had already proven.
-->

---

# After patterns: matching carries data

```csharp
return shape switch
{
    Circle c => Math.PI * c.Radius * c.Radius,
    Rectangle r => r.Width * r.Height,
    _ => throw new NotSupportedException()
};
```

<!--
Speaker notes:
- This introduces the path toward modern pattern matching and unions.
-->

---

<!-- _class: section -->

# Null Stops Being Tribal Knowledge

.NET Core 3.x + C# 8

<div class="big-number">2019</div>

<!--
Speaker notes:
- Nullable reference types deserve significant time.
-->

---

# For years, this lied

```csharp
public string Name { get; set; }
```

<div class="callout">It looked required. It could still be null.</div>

<!--
Speaker notes:
- Strong phrase but fair.
- C# reference types always allowed null; the type did not communicate intent.
-->

---

# Nullable reference types changed the contract

<div class="columns">
<div>
<span class="compare-label">Maybe?</span>

```csharp
public string? MiddleName { get; set; }
```
</div>
<div>
<span class="compare-label">Expected</span>

```csharp
public string Name { get; set; }
```
</div>
</div>

<!--
Speaker notes:
- Nullable annotations are documentation the compiler can reason about.
- Make it clear they are warnings, not runtime enforcement.
-->

---

# We stopped hiding assumptions

```csharp
public Order CreateOrder(Customer customer, Address? shippingAddress)
{
    ArgumentNullException.ThrowIfNull(customer);

    return shippingAddress is null
        ? Order.Pickup(customer)
        : Order.Ship(customer, shippingAddress);
}
```

<!--
Speaker notes:
- Nullability works best with API design, guards, and domain modeling.
- The types tell callers what the method expects.
-->

---

# What became normal?

<div class="punchline">Nullability became part of the conversation.</div>

<!--
Speaker notes:
- Transition: Now move from absence to modeling data more clearly.
-->

---

<!-- _class: section -->

# Data Gets a Better Shape

.NET 5 + C# 9

<div class="big-number">2020</div>

<!--
Speaker notes:
- This is records, init-only setters, and top-level statements.
-->

---

# Before records: DTO ceremony

```csharp
public class CustomerSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public override bool Equals(object? obj) { /* ... */ }
    public override int GetHashCode() { /* ... */ }
}
```

<!--
Speaker notes:
- You don't need to show a full equality implementation. The comment is enough.
- The point is ceremony and mutability by default.
- Constraint: value equality, non-destructive copying, and practical immutability all had to be written by hand, on every type that needed them.
- And `GetHashCode` has to stay consistent with `Equals` or dictionaries and sets quietly misbehave. That's easy to get wrong, so most teams skipped it and lived with reference equality.
-->

---

# After records: data is obvious

```csharp
public record CustomerSummary(
    int Id,
    string Name);
```

<!--
Speaker notes:
- Records made value-like objects easy.
- Combined with init-only properties, immutable models felt lighter.
-->

---

# What became normal?

<div class="punchline">We stopped writing so much ceremony to describe simple data.</div>

<!--
Speaker notes:
- Transition: If C# 9 made data lighter, .NET 6 made application startup lighter.
-->

---

<!-- _class: section -->

# Ceremony Gets Deleted

.NET 6 + C# 10

<div class="big-number">2021</div>

<!--
Speaker notes:
- Note the numbering shift: .NET 6 with C# 10.
- Main idea: minimal APIs, global usings, file-scoped namespaces.
-->

---

# Before minimal APIs

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(e => e.MapControllers());
    }
}
```

<!--
Speaker notes:
- Acknowledge this pattern was powerful and still appropriate for many apps.
- But for many services, it was more ceremony than signal.
- Constraint: C# had no top-level statements until 9, so every entry point needed a class with methods. The host found `Startup` by reflection over a naming convention.
- The `ConfigureServices` / `Configure` split is a real two-phase bootstrap: register services, then build the pipeline from the built container.
- Minimal APIs removed the two methods, not the two phases. Worth saying — it keeps this from sounding like the old pattern was pointless.
-->

---

# After minimal APIs

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/orders/{id}", async (int id, IOrderStore store) =>
    await store.GetAsync(id));

app.Run();
```

<!--
Speaker notes:
- This is the slide where people usually nod.
- It captures modern .NET's preference for lower ceremony startup.
-->

---

# File-scoped namespaces removed indentation tax

<div class="columns">
<div>
<span class="compare-label">Old default</span>

```csharp
namespace Orders.Api
{
    public class OrderService
    {
    }
}
```
</div>
<div>
<span class="compare-label">New default</span>

```csharp
namespace Orders.Api;

public class OrderService
{
}
```
</div>
</div>

<!--
Speaker notes:
- Small feature, big daily impact.
- It is a great example of deleting code that never carried meaning.
-->

---

# What became normal?

<div class="punchline">The default template got out of the way.</div>

<!--
Speaker notes:
- Transition: after removing ceremony, newer releases improve expressiveness and correctness.
-->

---

<!-- _class: section -->

# More Intent in the Type System

.NET 7 + C# 11

<div class="big-number">2022</div>

<!--
Speaker notes:
- Main ideas: required members, raw string literals, generic math.
-->

---

# Required members made initialization explicit

<div class="columns">
<div>
<span class="compare-label">Old default</span>

```csharp
public class Customer
{
    public string Name { get; set; } = "";
}

var customer = new Customer();
```
</div>
<div>
<span class="compare-label">New default</span>

```csharp
public class Customer
{
    public required string Name { get; init; }
}

var customer = new Customer { Name = "Ada" };
```
</div>
</div>

<!--
Speaker notes:
- This complements nullable reference types.
- It communicates construction requirements directly.
-->

---

# Raw strings made text less hostile

```csharp
var json = """
{
  "name": "Ada",
  "role": "Engineer"
}
""";
```

<!--
Speaker notes:
- This is a practical applause feature.
- Great for JSON, SQL, regex-ish content, and generated snippets.
-->

---

# Generic math opened new libraries

```csharp
static T Add<T>(T left, T right)
    where T : INumber<T>
{
    return left + right;
}
```

<!--
Speaker notes:
- This is less everyday app code, but huge for library authors.
- It shows the type system getting more expressive.
-->

---

# What became normal?

<div class="punchline">More rules moved from comments and conventions into code.</div>

<!--
Speaker notes:
- Transition: compile-time work becomes a main story in .NET 8.
-->

---

<!-- _class: section -->

# Runtime Work Moves to Build Time

.NET 8 + C# 12

<div class="big-number">2023</div>

<!--
Speaker notes:
- Main ideas: source generators, Native AOT, primary constructors, collection expressions.
-->

---

# Reflection-heavy code had a cost

```csharp
var value = typeof(Customer)
    .GetProperty("Name")!
    .GetValue(customer);
```

<div class="callout">Flexible, powerful, and often invisible until startup, trimming, or AOT.</div>

<!--
Speaker notes:
- Reflection isn't bad. It is a tradeoff.
- The modern platform increasingly wants predictable, analyzable code.
-->

---

# Source generators changed the bargain

```csharp
[JsonSerializable(typeof(Customer))]
internal partial class AppJsonContext : JsonSerializerContext
{
}

var json = JsonSerializer.Serialize(
    customer,
    AppJsonContext.Default.Customer);
```

<!--
Speaker notes:
- Build-time generation can improve startup, trimming, and AOT compatibility.
- This is one of the key modern shifts: work moves earlier.
-->

---

# Native AOT rewards explicit code

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
</PropertyGroup>
```

<div class="callout">The runtime asks: “Can I know enough about your app before it runs?”</div>

<!--
Speaker notes:
- Keep this conceptual unless the audience is deep into AOT.
- Explain that AOT changes library and app design pressure.
-->

---

# Primary constructors shortened simple types

```csharp
public class OrderService(
    IOrderRepository repository,
    ILogger<OrderService> logger)
{
    public Task<Order?> GetAsync(int id) =>
        repository.GetAsync(id);
}
```

<!--
Speaker notes:
- This is another ceremony-reduction feature.
- It also changes how DI-heavy services look.
-->

---

# Collection expressions made shape lighter

```csharp
int[] numbers = [1, 2, 3, 4];

List<string> names = ["Ada", "Grace", "Barbara"];
```

<!--
Speaker notes:
- Another quality-of-life feature.
- The language keeps making common shapes easier to read.
-->

---

# What became normal?

<div class="punchline">We stopped paying at runtime for work the compiler could do.</div>

<!--
Speaker notes:
- Transition: newer releases refine these ideas and push them further.
-->

---

<!-- _class: section -->

# Refinement and Direction

.NET 9 / 10 + C# 13 / 14

<div class="big-number">2024</div>

<!--
Speaker notes:
- This should be brief. Avoid trying to exhaustively cover current features.
-->

---

# Not every release rewrites our habits

<div class="callout">Some releases make the existing style smoother, safer, and more complete.</div>

<!--
Speaker notes:
- This prevents the deck from pretending every year is a revolution.
- It also protects pacing.
-->

---

# C# 13 and 14: polish with a direction

- More flexible `params`
- Better locking patterns
- Extension members
- Partial members improvements
- More places for the compiler to understand intent

<!--
Speaker notes:
- Keep this high-level unless you want to add concrete samples later.
- The narrative is refinement toward expressive APIs.
-->

---

# What became normal?

<div class="punchline">The language keeps removing friction from patterns we already use.</div>

<!--
Speaker notes:
- Transition: now the future-facing section: .NET 11 and C# 15.
-->

---

<!-- _class: section -->

# Where This Points Next

.NET 11 preview + C# 15

<div class="big-number">2026</div>

<!--
Speaker notes:
- Be explicit that this is future-facing / preview depending on timing.
- The main story is union types.
-->

---

# Today: hand-rolled result types

```csharp
public abstract record Result<T>;

public record Success<T>(T Value) : Result<T>;
public record Failure<T>(string Error) : Result<T>;
```

<!--
Speaker notes:
- Many teams already do this with records, OneOf, FluentResults, custom Result types, or exceptions.
- The pain is not that it is impossible; it is that it is convention-heavy.
-->

---

# C# 15: union types

```csharp
public record Success<T>(T Value);
public record Failure(string Error);

public union Result<T>(Success<T>, Failure);
```

<!--
Speaker notes:
- Treat syntax as current/future-facing and subject to change if the language evolves.
- The concept is closed alternatives the compiler can reason about.
-->

---

# Why unions fit the story

<div class="punchline">They are another step toward making invalid states harder to express.</div>

<!--
Speaker notes:
- Connect back to generics, nullable, required members, pattern matching.
- Each feature makes more intent visible to tools and readers.
-->

---

# The arc of .NET code

<div class="timeline-grid">
<div class="tile"><div class="era">Generics</div><div class="what">no mystery objects</div></div>
<div class="tile"><div class="era">LINQ</div><div class="what">intent over loops</div></div>
<div class="tile"><div class="era">async</div><div class="what">work over threads</div></div>
<div class="tile"><div class="era">Nullable</div><div class="what">contracts over assumptions</div></div>
<div class="tile"><div class="era">Records</div><div class="what">data over ceremony</div></div>
<div class="tile"><div class="era">Minimal APIs</div><div class="what">signal over scaffolding</div></div>
<div class="tile"><div class="era">AOT</div><div class="what">compile-time over runtime</div></div>
<div class="tile"><div class="era">Unions</div><div class="what">states over flags</div></div>
</div>

<!--
Speaker notes:
- This is a summary slide. Slow down.
- The arc is not “new is always better”; the arc is “intent becomes visible.”
-->

---

# The code we don't write anymore

| Era | We stopped writing |
|---|---|
| .NET 2 | casts around collections |
| .NET 3.5 | many manual loops |
| C# 5 | callback pyramids |
| .NET Core | Windows-only assumptions |
| C# 8 | undocumented null assumptions |
| .NET 6 | startup ceremony |
| .NET 8+ | some runtime discovery work |

<!--
Speaker notes:
- This is the recap aligned to the title.
- Feel free to add your own examples from consulting/projects.
-->

---

# None of this was free

<div class="timeline-grid">
<div class="tile"><div class="era">LINQ</div><div class="what">hides an N+1 in plain sight</div></div>
<div class="tile"><div class="era">async</div><div class="what">colors every method it touches</div></div>
<div class="tile"><div class="era">Nullable</div><div class="what">warnings you learned to ignore</div></div>
<div class="tile"><div class="era">AOT</div><div class="what">trimming deletes what it can't see</div></div>
</div>

<!--
Speaker notes:
- Purpose: the deck has been a story of improvement. This is the honesty beat that makes the rest credible.
- LINQ: deferred execution and per-element allocation are invisible at the call site. Fine until it is a hot path, or an IQueryable quietly doing N+1.
- async: "async all the way" is not a style preference, it is a constraint. And .Result on an async method is still deadlocking apps.
- Nullable: annotations are only as good as the discipline around them. The ! operator silences the compiler without changing the runtime, and warning fatigue turns the feature into decoration.
- AOT and source generators: reflection was never free — it always cost lookup, allocation, and lost inlining. What changed is the guarantee. The compiler can't see a reflective call, so the trimmer removes the type as unused and you find out in production. The cost moved from performance to correctness.
- Also worth saying: generated code is harder to step through than the reflection it replaced.
- Do not let this become a complaint. Framing: these are the bills that came with genuinely better defaults, and every one is worth paying.
- Transition: which is exactly why this is a history of defaults, not features.
-->

---

# The real history of .NET

<div class="punchline">Not just new features.</div>

<div class="punchline accent">New defaults.</div>

<!--
Speaker notes:
- Pause here.
- Then deliver the closing thought: great .NET developers recognize when yesterday's best practice becomes today's unnecessary code.
-->

---

# Closing thought

<div class="punchline">The best .NET developers recognize when yesterday’s best practice has become today’s unnecessary code.</div>

<!--
Speaker notes:
- End on this.
- Optional final line: “That’s the code we don’t write anymore.”
-->

---

# Discussion

<div class="big center">What code did a release make <span class="accent">you</span> stop writing?</div>

<!--
Speaker notes:
- Use this if time allows for Q&A or audience interaction.
- Good prompt for user group setting.
-->
