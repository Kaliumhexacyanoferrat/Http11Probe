---
title: "LONG-METHOD"
description: "LONG-METHOD test documentation"
weight: 5
---

| | |
|---|---|
| **Test ID** | `MAL-LONG-METHOD` |
| **Category** | Malformed Input |
| **Expected** | `400` or close |

## What it sends

A request with a ~100 KB method name.

```http
AAAA...{100,000 × 'A'}... / HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

The HTTP method is 100,000 bytes of `A` characters.


## Why it matters

Methods are tokens with no defined maximum length, but 100 KB exceeds any reasonable limit. A server that buffers this risks memory exhaustion.

## Sources

- [RFC 9110 Section 9.1](https://www.rfc-editor.org/rfc/rfc9110#section-9.1)
