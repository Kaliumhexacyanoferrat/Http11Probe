---
title: "CHUNK-EXT-CTRL"
description: "CHUNK-EXT-CTRL test documentation"
weight: 28
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-EXT-CTRL` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

A chunked request with a NUL byte (`0x00`) embedded in the chunk extension: `5;\x00ext`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5;\x00ext\r\n
hello\r\n
0\r\n
\r\n
```

The chunk extension contains a NUL byte (`\x00`) before `ext`.


## What the RFC says

Chunk extension names must be `token` characters, and extension values must be either `token` or `quoted-string`. NUL and other control characters (except HTAB) are not valid in either production.

## Why it matters

NUL bytes in chunk extensions can cause parsers to truncate or misinterpret the extension, leading to disagreements about chunk boundaries. C-based string functions often treat NUL as a string terminator, creating divergent behavior between parsers.

## Sources

- [RFC 9112 Section 7.1.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1)
