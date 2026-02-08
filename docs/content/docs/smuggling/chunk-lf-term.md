---
title: "CHUNK-LF-TERM"
description: "CHUNK-LF-TERM test documentation"
weight: 27
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-LF-TERM` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

A chunked request where the chunk data terminator is a bare `LF` (`\n`) instead of `CRLF` (`\r\n`): `5\r\nhello\n0\r\n\r\n`.

## What the RFC says

> "Each chunk ends with CRLF." — RFC 9112 Section 7.1

The CRLF after chunk data is mandatory. A bare LF violates the chunked transfer coding grammar.

## Why it matters

If one parser accepts bare LF as a chunk data terminator and another requires strict CRLF, they disagree on where the chunk data ends. The byte that the strict parser considers part of chunk data is treated as the next chunk-size line by the lenient parser — a classic desynchronization vector.

## Sources

- [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
- [RFC 9112 Section 2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2)
