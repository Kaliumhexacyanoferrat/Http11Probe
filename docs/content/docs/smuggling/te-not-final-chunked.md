---
title: "TE-NOT-FINAL-CHUNKED"
description: "TE-NOT-FINAL-CHUNKED test documentation"
weight: 16
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-NOT-FINAL-CHUNKED` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §6.3](https://www.rfc-editor.org/rfc/rfc9112#section-6.3) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

`Transfer-Encoding: chunked, gzip` — chunked is not the final encoding.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked, gzip\r\n
\r\n
0\r\n
\r\n
```


## What the RFC says

> "If a Transfer-Encoding header field is present in a request and the chunked transfer coding is not the final encoding, the message body length cannot be determined reliably; the server MUST respond with the 400 (Bad Request) status code and then close the connection." — RFC 9112 §6.3

This is MUST-level language — servers have no discretion here.

## Why it matters

If chunked isn't the final encoding, the server cannot determine body boundaries. This can be exploited for smuggling.

## Sources

- [RFC 9112 §6.3](https://www.rfc-editor.org/rfc/rfc9112#section-6.3)
- [RFC 9112 §7](https://www.rfc-editor.org/rfc/rfc9112#section-7)
