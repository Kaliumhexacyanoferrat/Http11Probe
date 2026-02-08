---
title: "NUL-IN-HEADER-VALUE"
description: "NUL-IN-HEADER-VALUE test documentation"
weight: 15
---

| | |
|---|---|
| **Test ID** | `MAL-NUL-IN-HEADER-VALUE` |
| **Category** | Malformed Input |
| **Expected** | `400` or close |

## What it sends

A request with a NUL byte (0x00) embedded in a header value.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-Test: val\x00ue\r\n
\r\n
```

The header value contains a NUL byte (`\x00`) between `val` and `ue`.


## Why it matters

NUL bytes are not valid in HTTP header field values. They can cause string truncation in C-based parsers, potentially hiding or injecting header content. A robust server must reject any request containing NUL bytes in headers.

## Sources

- RFC 9110 Section 5.5 — field values must not contain NUL
