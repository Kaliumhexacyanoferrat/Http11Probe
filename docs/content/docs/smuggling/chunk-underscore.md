---
title: "CHUNK-UNDERSCORE"
description: "CHUNK-UNDERSCORE test documentation"
weight: 21
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-UNDERSCORE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

Chunk size `1_0` — with underscore separator.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
1_0\r\n
hello world!!!!!\r\n
0\r\n
\r\n
```

The chunk size `1_0` uses an underscore separator (like numeric literals in some languages).


## What the RFC says

> Chunk size is `1*HEXDIG`. Underscores are not hex digits.

## Why it matters

Some language parsers accept `_` in numeric literals. If a server parses `1_0` as 10, it reads more data than intended.

## Sources

- [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
