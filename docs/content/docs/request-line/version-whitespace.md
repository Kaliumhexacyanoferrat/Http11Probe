---
title: "VERSION-WHITESPACE"
description: "VERSION-WHITESPACE test documentation"
weight: 17
---

| | |
|---|---|
| **Test ID** | `COMP-VERSION-WHITESPACE` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §2.3](https://www.rfc-editor.org/rfc/rfc9112#section-2.3) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A request with `HTTP/ 1.1` as the version -- a space character inserted between `HTTP/` and `1.1`.

```http
GET / HTTP/ 1.1\r\n
Host: localhost:8080\r\n
\r\n
```

## What the RFC says

> "HTTP-version = HTTP-name '/' DIGIT '.' DIGIT" -- RFC 9112 Section 2.3

The HTTP-version is a single contiguous token with no internal whitespace. The space between the slash and the version digits breaks the token, making the request-line invalid. The grammar does not allow any SP or HTAB inside the version string.

## Why it matters

A server that is lenient about whitespace inside the version token could be tricked into parsing the request-line differently than a strict proxy in front of it. For example, a lenient parser might read `HTTP/` followed by ` 1.1` and strip the space, while a strict parser sees an invalid version. This differential creates opportunities for request smuggling.

## Sources

- [RFC 9112 §2.3 -- HTTP Version](https://www.rfc-editor.org/rfc/rfc9112#section-2.3)
