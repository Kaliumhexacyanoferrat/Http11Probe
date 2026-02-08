---
title: "CHUNK-NEGATIVE"
description: "CHUNK-NEGATIVE test documentation"
weight: 31
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-NEGATIVE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

A chunked request with a negative chunk size: `-1\r\nhello\r\n0\r\n\r\n`.

## What the RFC says

> "chunk-size = 1*HEXDIG" — RFC 9112 Section 7.1

The chunk size grammar only allows hexadecimal digits (0-9, A-F, a-f). A minus sign is not a valid HEXDIG character.

## Why it matters

A parser that interprets `-1` as a signed integer may wrap it to a very large unsigned value, causing it to read far beyond the actual data. This can lead to buffer over-reads, denial of service, or desynchronization with a stricter parser that rejects the negative value.

## Sources

- [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
