---
title: "WHITESPACE-BEFORE-HEADERS"
description: "WHITESPACE-BEFORE-HEADERS test documentation"
weight: 6
---

| | |
|---|---|
| **Test ID** | `COMP-WHITESPACE-BEFORE-HEADERS` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 Section 2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2) |
| **Requirement** | SHOULD reject |
| **Expected** | `400` or close |

## What it sends

A request with whitespace (SP) before the first header line, between the request-line and the headers.

```http
GET / HTTP/1.1\r\n
 \r\n
Host: localhost:8080\r\n
\r\n
```

A line with a single space appears between the request-line and the first header.


## What the RFC says

> "A recipient that receives whitespace between the start-line and the first header field MUST either reject the message as invalid or consume each whitespace-preceded line without further processing of it." — RFC 9112 Section 2.2

## Why it matters

Whitespace before headers can confuse parsers about where headers begin, potentially enabling smuggling.

## Sources

- [RFC 9112 Section 2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2)
