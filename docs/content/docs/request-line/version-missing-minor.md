---
title: "VERSION-MISSING-MINOR"
description: "VERSION-MISSING-MINOR test documentation"
weight: 15
---

| | |
|---|---|
| **Test ID** | `COMP-VERSION-MISSING-MINOR` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §2.3](https://www.rfc-editor.org/rfc/rfc9112#section-2.3) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A request with `HTTP/1` as the version -- missing the dot and minor version digit.

```http
GET / HTTP/1\r\n
Host: localhost:8080\r\n
\r\n
```

## What the RFC says

> "HTTP-version = HTTP-name '/' DIGIT '.' DIGIT" -- RFC 9112 Section 2.3

The HTTP version string requires exactly one digit, a dot, and one digit after `HTTP/`. `HTTP/1` omits the dot and the minor version digit entirely, so it does not match the grammar. A server that receives this cannot determine the protocol version and should reject the request.

## Why it matters

A truncated version string creates ambiguity about the client's capabilities. If a server guesses the minor version (e.g., assumes `HTTP/1.0` or `HTTP/1.1`), it may enable or disable features like persistent connections, chunked encoding, or Host header requirements incorrectly. Strict parsing prevents this guesswork.

## Sources

- [RFC 9112 §2.3 -- HTTP Version](https://www.rfc-editor.org/rfc/rfc9112#section-2.3)
