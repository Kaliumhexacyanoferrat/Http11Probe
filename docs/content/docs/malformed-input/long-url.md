---
title: "LONG-URL"
description: "LONG-URL test documentation"
weight: 2
---

| | |
|---|---|
| **Test ID** | `MAL-LONG-URL` |
| **Category** | Malformed Input |
| **RFC** | [RFC 9110 Section 4.1](https://www.rfc-editor.org/rfc/rfc9110#section-4.1) |
| **Expected** | `400`, `414`, `431`, or close |

## What it sends

A request with a ~100 KB URL.

```http
GET /AAAA...{100,000 × 'A'}... HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

The URL path is 100,001 bytes long (a `/` followed by 100,000 `A` characters).


## What the RFC says

> "A server that receives a request-target longer than any URI it wishes to parse **MUST** respond with a 414 (URI Too Long) status code."

## Sources

- [RFC 9110 Section 15.5.15](https://www.rfc-editor.org/rfc/rfc9110#section-15.5.15)
