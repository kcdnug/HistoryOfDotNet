# Talk Overview: The Code We Don't Write Anymore

## Core premise

This talk is not a tour of release notes. It is a chronological story about how .NET and C# changed the default shape of everyday code. The recurring question is:

> What code did this release make us stop writing?

The best slides should show a concrete “before” and “after,” then name the mindset shift. Avoid cramming every feature into the deck. Mention secondary features only when they help explain the larger change.

## Intended audience

.NET developers with some professional experience. The audience may include people who started in .NET Framework, .NET Core, or modern .NET. Assume most people know the major features but have not thought about them as a historical sequence.

## Tone

Conversational, reflective, practical, lightly humorous. The talk should feel like a guided walk through code we once considered normal, not a lecture about how old code was bad.

Preferred framing:

- “This was good code for its time.”
- “The platform taught us a new default.”
- “The compiler took over work we used to do by hand.”
- “We stopped writing ceremony and started expressing intent.”

Avoid:

- Mocking older code or older developers.
- Turning the deck into a feature checklist.
- Spending too long on releases that did not change everyday habits.
- Overclaiming future features that are still in preview.

## Target length

45–60 minutes.

Recommended timing:

- Opening and thesis: 5 minutes
- .NET Framework 1.0–2.0: 8 minutes
- LINQ era: 8 minutes
- async era: 8 minutes
- .NET Core/unification: 6 minutes
- Nullable, records, pattern matching: 8 minutes
- Minimal APIs, source generators, Native AOT: 8 minutes
- .NET 11 / C# 15 and closing: 5 minutes

## Narrative spine

1. .NET 1.0: Everything was explicit.
2. Generics: We stopped programming against `object`.
3. LINQ: We stopped thinking first in loops.
4. async/await: We stopped manually coordinating callbacks and continuations.
5. .NET Core: We stopped assuming Windows, GAC, and one machine-wide framework.
6. Nullable reference types: We stopped treating nullability as tribal knowledge.
7. Records/patterns: We started modeling shape and intent more directly.
8. Minimal APIs: We deleted ceremony.
9. Source generators and AOT: We moved work from runtime to build time.
10. Union types: We keep moving toward making illegal states unrepresentable.

## Slide structure pattern

For major features, use this rhythm:

1. What code looked like before.
2. What changed in the platform or language.
3. What code looks like after.
4. What became normal.

## Future-edit guidance

When editing with another AI agent, ask it to preserve:

- The title and abstract unless explicitly revising them.
- The chronological framework/language pairing.
- Large, readable code samples.
- Speaker notes on every slide.
- The recurring “what became normal?” framing.
- The distinction between released features and preview/future-facing features.

When adding a release, do not add it just because it exists. Add it only if it helps the thesis: code we no longer write, assumptions we no longer make, or work moved from developer/runtime/compiler.

## Current-version caution

As of the deck creation date, .NET 11 is preview/future-facing and should be discussed cautiously. C# 15 union types should be framed as directionally important rather than as a long-settled everyday coding style.
