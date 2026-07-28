# The Code We Don't Write Anymore

This package contains an AI-editable Marp slide deck for the talk **The Code We Don't Write Anymore: How .NET Changed the Way We Think About Code**.

## Primary files

- `slides/deck.md` — the slide deck source, including presenter notes in HTML comments.
- `slides/theme.css` — custom Marp theme.
- `TALK-OVERVIEW.md` — source-of-truth overview for future editing sessions.
- `ABSTRACT.md` — title and original abstract.
- `STYLE-GUIDE.md` — formatting, voice, and edit guidance.
- `TIMELINE.md` — framework/language chronology and “what became normal” themes.
- `snippets/` — standalone code samples from the deck.
- `references.md` — external sources used to confirm current/future-version claims.

## Recommended workflow

Install dependencies once:

```bash
npm install
```

Then start a live preview at <http://localhost:8080>:

```bash
npm start
```

The preview re-renders automatically when `slides/deck.md` or `slides/theme.css` changes. The VS Code **Marp for VS Code** extension also works if you prefer previewing inside the editor. The deck is intentionally plain Markdown so another AI agent can edit it safely.

To export:

```bash
npm run pptx
npm run pdf
npm run html
```

Speaker notes are stored as HTML comments directly under each slide. In Marp presenter mode, they appear as notes.
