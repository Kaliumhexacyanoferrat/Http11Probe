---
title: "CHUNK-EXTENSION-LONG"
description: "CHUNK-EXTENSION-LONG test documentation"
weight: 18
---

| | |
|---|---|
| **Test ID** | `MAL-CHUNK-EXTENSION-LONG` |
| **Category** | Malformed Input |
| **Expected** | `400`/`431` or close |

## What it sends

A chunked request with a chunk extension containing 100KB of data.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5;ext=AAAA...{100,000 × 'A'}...\r\n
hello\r\n
0\r\n
\r\n
```

The chunk extension value is 100,000 bytes of `A` characters.


## Why it matters

While chunk extensions are syntactically valid per RFC 9112 Section 7.1.1, a 100KB extension is pathological. A robust server should reject unreasonably large chunk extensions to prevent resource exhaustion and denial of service.

## Sources

- RFC 9112 Section 7.1.1 — chunk extensions
