---
title: "HEADER-NO-COLON"
description: "HEADER-NO-COLON test documentation"
weight: 5
---

| | |
|---|---|
| **Test ID** | `RFC9112-5-HEADER-NO-COLON` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 Section 5](https://www.rfc-editor.org/rfc/rfc9112#section-5) |
| **Requirement** | Implicit MUST (grammar violation) |
| **Expected** | `400` or close |

## What it sends

A header line with no colon: `InvalidHeaderNoColon`.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
NoColonHere\r\n
\r\n
```

A header line without any colon separator.


## What the RFC says

The field-line grammar is `field-name ":" OWS field-value OWS`. A line without a colon does not match this grammar. It could be misinterpreted as a continuation line, a new request, or garbage — any of which is dangerous.

## Sources

- [RFC 9112 Section 5 — Field Syntax](https://www.rfc-editor.org/rfc/rfc9112#section-5)
