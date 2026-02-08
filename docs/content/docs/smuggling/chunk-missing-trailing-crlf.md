---
title: "CHUNK-MISSING-TRAILING-CRLF"
description: "CHUNK-MISSING-TRAILING-CRLF test documentation"
weight: 29
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-MISSING-TRAILING-CRLF` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

Chunk data without the required trailing CRLF after data.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello0\r\n
\r\n
```

The chunk data `hello` is not followed by `\r\n` — the `0` terminator runs directly into the chunk data, reading as `hello0`.


## What the RFC says

> Each chunk is `chunk-size CRLF chunk-data CRLF`. The trailing CRLF is mandatory.

## Why it matters

Missing trailing CRLF can cause parsers to read into the next chunk header as data or vice versa.

## Sources

- [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
