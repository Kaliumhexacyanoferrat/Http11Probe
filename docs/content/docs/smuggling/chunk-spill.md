---
title: "CHUNK-SPILL"
description: "CHUNK-SPILL test documentation"
weight: 26
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-SPILL` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

A chunked request that declares chunk size `5` but sends 7 bytes of data (`hello!!`), followed by the terminator.

## What the RFC says

> "The chunk-data is a sequence of chunk-size octets." — RFC 9112 Section 7.1

The server must read exactly `chunk-size` bytes of data, then expect `CRLF`. If the data exceeds the declared size, the trailing bytes land where the CRLF should be, corrupting the framing.

## Why it matters

An oversized chunk is a framing violation. If a lenient parser reads past the declared size, it desynchronizes from a strict parser that reads exactly `chunk-size` bytes. This discrepancy is exploitable for smuggling the excess bytes as part of the next request.

## Sources

- [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
