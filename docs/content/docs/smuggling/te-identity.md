---
title: "TE-IDENTITY"
description: "TE-IDENTITY test documentation"
weight: 30
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-IDENTITY` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 Section 7](https://www.rfc-editor.org/rfc/rfc9112#section-7) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

A request with `Transfer-Encoding: identity` and `Content-Length: 5`. The `identity` encoding was deprecated and removed in HTTP/1.1 (RFC 7230 and later RFC 9112).

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: identity\r\n
Content-Length: 5\r\n
\r\n
hello
```


## What the RFC says

> "The 'identity' transfer coding was used in previous versions of this specification to mean 'no encoding'... but has since been removed." — RFC 9112 Section 7

Only `chunked` is defined as a transfer coding for HTTP/1.1 requests. The presence of `Transfer-Encoding: identity` alongside `Content-Length` creates ambiguous framing.

## Why it matters

If a front-end treats `identity` as "no encoding" and uses Content-Length, but a back-end rejects the unknown TE, they disagree on how to parse the body. Conversely, if the back-end ignores the TE header entirely, CL is used — but a front-end that rejects may not forward the request at all. Both scenarios create desync potential.

## Sources

- [RFC 9112 Section 7](https://www.rfc-editor.org/rfc/rfc9112#section-7)
