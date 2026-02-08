---
title: "TRACE-WITH-BODY"
description: "TRACE-WITH-BODY test documentation"
weight: 20
---

| | |
|---|---|
| **Test ID** | `COMP-TRACE-WITH-BODY` |
| **Category** | Compliance |
| **RFC** | [RFC 9110 §9.3.8](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.8) |
| **Requirement** | SHOULD reject (unscored) |
| **Expected** | `400`/`405` = Pass, `200` = Warn |

## What it sends

A TRACE request that includes a `Content-Length` header and a message body, which clients are prohibited from sending.

```http
TRACE / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 5\r\n
\r\n
hello
```

## What the RFC says

> "A client MUST NOT send content in a TRACE request." -- RFC 9110 Section 9.3.8

While the prohibition is stated as a client requirement, a server receiving a TRACE request with a body is dealing with a client that has violated the spec. The server should reject the request or ignore the body entirely.

**Pass:** Server rejects with `400` (bad request) or `405` (method not allowed) or `501` (not implemented).
**Warn:** Server accepts with `200` (processes the TRACE despite the body).

## Why it matters

TRACE is designed to echo back the request headers for diagnostic purposes. If a server processes a TRACE request with a body, the body content could be reflected back or logged, potentially amplifying Cross-Site Tracing (XST) attacks. A body in a TRACE request is always a sign of a misbehaving or malicious client, and the safest response is rejection.

## Sources

- [RFC 9110 §9.3.8 -- TRACE](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.8)
- [OWASP: Cross-Site Tracing](https://owasp.org/www-community/attacks/Cross_Site_Tracing)
