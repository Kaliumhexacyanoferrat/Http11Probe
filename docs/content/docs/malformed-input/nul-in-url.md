---
title: "NUL-IN-URL"
description: "NUL-IN-URL test documentation"
weight: 7
---

| | |
|---|---|
| **Test ID** | `MAL-NUL-IN-URL` |
| **Category** | Malformed Input |
| **Expected** | `400` or close |

## What it sends

A request with a NUL byte (`\x00`) embedded in the URL.

```http
GET /\x00test HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

The URL contains a NUL byte (`\x00`) between `/` and `test`.


## Why it matters

NUL bytes terminate strings in C/C++. A NUL in the URL could cause path truncation in backend systems, allowing path traversal or access to unintended resources.

## Sources

- [RFC 9112 Section 3](https://www.rfc-editor.org/rfc/rfc9112#section-3)
