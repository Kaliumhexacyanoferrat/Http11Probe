---
title: "CHUNK-EXT-LF"
description: "CHUNK-EXT-LF test documentation"
weight: 25
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-EXT-LF` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §7.1.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1) |
| **Expected** | `400` or `2xx` |

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

Chunk extensions must follow the grammar `chunk-ext = *( BWS ";" BWS chunk-ext-name [ "=" chunk-ext-val ] )` with CRLF terminating the chunk line. However, RFC 9112 §2.2 states:

> "Although the line terminator for the start-line and fields is the sequence CRLF, a recipient MAY recognize a single LF as a line terminator and ignore any preceding CR."

This means a server MAY accept bare LF — both strict rejection and lenient acceptance are RFC-compliant.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid per §2.2 MAY accept bare LF).

## Why it matters

This is the **TERM.EXT** vector from chunked encoding research. If a parser accepts bare LF in chunk extensions, it may parse chunk boundaries differently from a strict parser, enabling desynchronization and smuggling.

## Sources

- [RFC 9112 §7.1.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1)
- [RFC 9112 §2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2)
