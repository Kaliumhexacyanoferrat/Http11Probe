---
title: "LONG-HEADER-VALUE"
description: "LONG-HEADER-VALUE test documentation"
weight: 4
---

| | |
|---|---|
| **Test ID** | `MAL-LONG-HEADER-VALUE` |
| **Category** | Malformed Input |
| **Expected** | `400`, `431`, or close |

## What it sends

A request with a ~100 KB header field value.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-Big: BBBB...{100,000 × 'B'}...\r\n
\r\n
```

The `X-Big` header value is 100,000 bytes of `B` characters.


## Sources

- [RFC 6585 Section 5](https://www.rfc-editor.org/rfc/rfc6585#section-5)
