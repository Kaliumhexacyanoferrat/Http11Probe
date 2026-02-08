---
title: "TE-LEADING-COMMA"
description: "TE-LEADING-COMMA test documentation"
weight: 23
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-LEADING-COMMA` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §5.6.1](https://www.rfc-editor.org/rfc/rfc9110#section-5.6.1) |
| **Expected** | `400` or `2xx` |

## What it sends

`Transfer-Encoding: , chunked` — leading comma before `chunked`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: , chunked\r\n
Content-Length: 5\r\n
\r\n
hello
```

The Transfer-Encoding value starts with a leading comma before `chunked`.


## What the RFC says

> "A recipient MUST parse and ignore a reasonable number of empty list elements." — RFC 9110 §5.6.1

The leading comma produces an empty list element before `chunked`. Since RFC 9110 §5.6.1 requires recipients to ignore empty list elements, a server that strips the empty element and processes `chunked` normally is RFC-compliant.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid per §5.6.1 empty-element handling).

## Why it matters

Some parsers strip leading commas and see "chunked", while others reject the value entirely. This discrepancy enables smuggling when front-end and back-end parsers disagree on whether Transfer-Encoding is valid.

## Sources

- [RFC 9110 §5.6.1 — Lists](https://www.rfc-editor.org/rfc/rfc9110#section-5.6.1)
- [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
