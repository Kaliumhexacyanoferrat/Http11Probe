---
title: "HOST-EMPTY-VALUE"
description: "HOST-EMPTY-VALUE test documentation"
weight: 6
---

| | |
|---|---|
| **Test ID** | `COMP-HOST-EMPTY-VALUE` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §3.2](https://www.rfc-editor.org/rfc/rfc9112#section-3.2) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A request with a `Host` header present but with an empty value.

```http
GET / HTTP/1.1\r\n
Host: \r\n
\r\n
```

The `Host` header line exists, but its value is empty (nothing between the colon and CRLF).

## What the RFC says

> "A server MUST respond with a 400 (Bad Request) status code to any HTTP/1.1 request message that lacks a Host header field and to any request message that contains more than one Host header field line or a Host header field with an invalid field value." -- RFC 9112 Section 3.2

An empty Host value is invalid when the request-target is in origin-form. The host identification is effectively absent, making the server unable to determine which virtual host is being addressed.

## Why it matters

A Host header with an empty value is functionally equivalent to having no Host header at all. If a server accepts this, it may fall back to a default virtual host, potentially serving content from an unintended application. In multi-tenant environments, this can lead to information disclosure or incorrect routing.

## Sources

- [RFC 9112 Section 3.2 -- Request Target](https://www.rfc-editor.org/rfc/rfc9112#section-3.2)
- [RFC 9110 Section 7.2 -- Host and :authority](https://www.rfc-editor.org/rfc/rfc9110#section-7.2)
