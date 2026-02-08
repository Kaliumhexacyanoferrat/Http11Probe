---
title: "HTTP10-DEFAULT-CLOSE"
description: "HTTP10-DEFAULT-CLOSE test documentation"
weight: 7
---

| | |
|---|---|
| **Test ID** | `COMP-HTTP10-DEFAULT-CLOSE` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §9.3](https://www.rfc-editor.org/rfc/rfc9112#section-9.3) |
| **Requirement** | SHOULD |
| **Expected** | `2xx` + connection closed |

## What it sends

An HTTP/1.0 GET request without a `Connection: keep-alive` header.

```http
GET / HTTP/1.0\r\n
Host: localhost:8080\r\n
\r\n
```

## What the RFC says

HTTP/1.0 connections are not persistent by default. Unlike HTTP/1.1, where persistent connections are the default, an HTTP/1.0 client must explicitly request persistence via `Connection: keep-alive`.

> "If the received protocol is HTTP/1.0, the 'close' connection option is to be assumed." -- RFC 9112 Section 9.3

Without `Connection: keep-alive`, the server should treat the connection as non-persistent and close it after delivering the response.

**Pass:** Server responds `2xx` and closes the connection.
**Warn:** Server responds `2xx` but keeps the connection open (minor violation of SHOULD).

## Why it matters

If a server treats HTTP/1.0 connections as persistent by default, it may hold the connection open indefinitely waiting for another request that will never come, wasting resources. More critically, in proxy chains, a downstream server keeping an HTTP/1.0 connection alive when the proxy expects it to close can cause response desynchronization.

## Sources

- [RFC 9112 §9.3 -- Persistence](https://www.rfc-editor.org/rfc/rfc9112#section-9.3)
