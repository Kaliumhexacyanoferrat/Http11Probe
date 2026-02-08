---
title: "CL-LEADING-ZEROS"
description: "CL-LEADING-ZEROS test documentation"
weight: 3
---

| | |
|---|---|
| **Test ID** | `RFC9112-6.1-CL-LEADING-ZEROS` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Expected** | `400` or close |

## What it sends

Content-Length with leading zeros: `Content-Length: 007`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 005\r\n
\r\n
hello
```


## What the RFC says

While `007` matches `1*DIGIT`, leading zeros create ambiguity. Some parsers may interpret as octal, some as decimal.

## Sources

- [RFC 9110 Section 8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
