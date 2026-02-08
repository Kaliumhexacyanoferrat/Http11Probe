---
title: "TE-TAB-BEFORE-VALUE"
description: "TE-TAB-BEFORE-VALUE test documentation"
weight: 56
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-TAB-BEFORE-VALUE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5) |
| **Requirement** | MAY |
| **Expected** | `400` or `2xx` |

## What it sends

Transfer-Encoding with a horizontal tab (HTAB) instead of space as the OWS separator before the value, alongside Content-Length.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding:\tchunked\r\n
Content-Length: 5\r\n
\r\n
hello
```

The tab character (`\t`, 0x09) separates the colon from `chunked` instead of the usual space (0x20).


## What the RFC says

> "OWS = *( SP / HTAB )" — RFC 9110 §5.5

Tab (HTAB) is explicitly defined as valid optional whitespace alongside space (SP). A compliant server should parse `Transfer-Encoding:\tchunked` identically to `Transfer-Encoding: chunked`.

**Pass:** Server rejects with `400` (strict, safe — some parsers only accept SP).
**Warn:** Server accepts and responds `2xx` (RFC-valid per OWS definition).

## Why it matters

Despite HTAB being valid OWS per the RFC, some real-world parsers only accept SP (0x20) as whitespace before the field value. When a front-end strips tabs or rejects the header while a back-end accepts it, they disagree on whether Transfer-Encoding is present — creating a CL/TE desync with Content-Length as the fallback.

## Sources

- [RFC 9110 §5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5)
- [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
