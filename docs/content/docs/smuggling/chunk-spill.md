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

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello!!\r\n
0\r\n
\r\n
```

The chunk size declares 5 bytes but the data is `hello!!` (7 bytes).


## What the RFC says

> "The chunk-data is a sequence of chunk-size octets." — RFC 9112 Section 7.1

The server must read exactly `chunk-size` bytes of data, then expect `CRLF`. If the data exceeds the declared size, the trailing bytes land where the CRLF should be, corrupting the framing.

## Why it matters

An oversized chunk is a framing violation. If a lenient parser reads past the declared size, it desynchronizes from a strict parser that reads exactly `chunk-size` bytes. This discrepancy is exploitable for smuggling the excess bytes as part of the next request.

## Sources

- [RFC 9112 Section 7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
