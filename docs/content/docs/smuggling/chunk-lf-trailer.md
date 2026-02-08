---
title: "CHUNK-LF-TRAILER"
description: "CHUNK-LF-TRAILER test documentation"
weight: 29
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-LF-TRAILER` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Expected** | `400` or `2xx` |

## What it sends

A chunked request where the final trailer section terminator uses bare `LF` instead of `CRLF`: `0\r\n\n` instead of `0\r\n\r\n`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello\r\n
0\r\n
\n
```

The final trailer terminator uses bare LF (`\n`) instead of CRLF (`\r\n`).


## What the RFC says

> "The trailer section is terminated by an empty line (CRLF)." — RFC 9112 §7.1

The chunked message ends with `last-chunk CRLF trailer-section CRLF`. Both CRLF sequences are mandatory per the grammar. However, RFC 9112 §2.2 states:

> "Although the line terminator for the start-line and fields is the sequence CRLF, a recipient MAY recognize a single LF as a line terminator and ignore any preceding CR."

This means a server MAY accept bare LF — both strict rejection and lenient acceptance are RFC-compliant.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (RFC-valid per §2.2 MAY accept bare LF).

## Why it matters

If a front-end parser accepts bare LF as the end of the chunked body but a back-end requires strict CRLF, the back-end may continue waiting for data or interpret subsequent bytes differently. This desync between message boundary detection is a smuggling vector.

## Sources

- [RFC 9112 §7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
- [RFC 9112 §2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2)
