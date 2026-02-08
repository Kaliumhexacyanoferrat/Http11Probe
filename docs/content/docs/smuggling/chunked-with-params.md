---
title: "CHUNKED-WITH-PARAMS"
description: "CHUNKED-WITH-PARAMS test documentation"
weight: 32
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNKED-WITH-PARAMS` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7](https://www.rfc-editor.org/rfc/rfc9112#section-7) |
| **Requirement** | Unscored |
| **Expected** | `400` or `2xx` |

## What it sends

`Transfer-Encoding: chunked;ext=val` — parameters on the chunked coding.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked;ext=val\r\n
Content-Length: 5\r\n
\r\n
hello
```

The `chunked` encoding name has parameters appended (`;ext=val`).


## What the RFC says

> Transfer coding parameters are not defined for `chunked` but the grammar technically allows them.

## Why it matters

This is unscored. Some servers strip parameters and treat it as chunked. Whether to accept or reject is implementation-dependent.

## Sources

- [RFC 9112 Section 7](https://www.rfc-editor.org/rfc/rfc9112#section-7)
