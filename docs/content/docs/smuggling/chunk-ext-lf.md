---
title: "CHUNK-EXT-LF"
description: "CHUNK-EXT-LF test documentation"
weight: 25
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-EXT-LF` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

A chunked request where the chunk extension area contains a bare `LF` instead of `CRLF`: `5;\nhello`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5;\n
hello\r\n
0\r\n
\r\n
```

The chunk size line `5;` is terminated with bare LF (`\n`) instead of CRLF.


## What the RFC says

Chunk extensions must follow the grammar `chunk-ext = *( BWS ";" BWS chunk-ext-name [ "=" chunk-ext-val ] )` with CRLF terminating the chunk line. A bare LF in the extension area violates the line terminator requirement of RFC 9112 Section 2.2.

## Why it matters

This is the **TERM.EXT** vector from chunked encoding research. If a parser accepts bare LF in chunk extensions, it may parse chunk boundaries differently from a strict parser, enabling desynchronization and smuggling.

## Sources

- [RFC 9112 Section 7.1.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1)
- [RFC 9112 Section 2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2)
