---
title: "REQUEST-LINE-TAB"
description: "REQUEST-LINE-TAB test documentation"
weight: 14
---

| | |
|---|---|
| **Test ID** | `COMP-REQUEST-LINE-TAB` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §3](https://www.rfc-editor.org/rfc/rfc9112#section-3) |
| **Requirement** | SHOULD reject, MAY accept |
| **Expected** | `400` or `2xx` |

## What it sends

A request-line that uses a horizontal tab (HT, 0x09) instead of a space (SP, 0x20) between the method and the request-target.

```http
GET\t/ HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

The `\t` between `GET` and `/` is a tab character, not a space.

## What the RFC says

The request-line grammar is `method SP request-target SP HTTP-version CRLF`, where `SP` is exactly one space (0x20). A tab does not match this grammar, making the request-line technically invalid.

However, RFC 9112 Section 3 also states:

> "Although the request-line grammar rule requires that each of the component elements be separated by a single SP octet, recipients MAY instead parse on whitespace-delimited word boundaries."

This means a server that treats tab as whitespace and successfully parses the request is also RFC-compliant.

**Pass:** Server rejects with `400` (strict, follows SHOULD).
**Warn:** Server accepts and responds `2xx` (RFC-valid per MAY parse leniently).

## Why it matters

If a front-end proxy collapses all whitespace (including tabs) while a back-end server only recognizes spaces, they may disagree on where the method, target, and version boundaries are. This kind of parser differential can be exploited for request smuggling or routing bypasses.

## Sources

- [RFC 9112 §3 -- Request Line](https://www.rfc-editor.org/rfc/rfc9112#section-3)
