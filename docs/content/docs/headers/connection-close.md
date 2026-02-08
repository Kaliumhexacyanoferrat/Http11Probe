---
title: "CONNECTION-CLOSE"
description: "CONNECTION-CLOSE test documentation"
weight: 6
---

| | |
|---|---|
| **Test ID** | `COMP-CONNECTION-CLOSE` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §9.3](https://www.rfc-editor.org/rfc/rfc9112#section-9.3) |
| **Requirement** | MUST |
| **Expected** | `2xx` + connection closed |

## What it sends

A standard GET request with `Connection: close` indicating the client wants the server to close the connection after sending the response.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
Connection: close\r\n
\r\n
```

## What the RFC says

> "A server that receives a 'close' connection option MUST initiate a close of the connection after it sends the final response to the request in which the 'close' was received." -- RFC 9112 Section 9.3

The server must both respond successfully and close the TCP connection afterward. Responding with `2xx` but leaving the connection open violates this requirement.

## Why it matters

If a server ignores `Connection: close` and keeps the connection alive, a client may send a second request on what it believes is a new connection. In proxy environments, this can lead to response mismatch: the proxy believes the connection is closed and assigns it to a different client, who then receives the first client's response. Honoring `Connection: close` is essential for correct connection lifecycle management.

## Sources

- [RFC 9112 §9.3 -- Persistence](https://www.rfc-editor.org/rfc/rfc9112#section-9.3)
- [RFC 9110 Section 7.6.1 -- Connection](https://www.rfc-editor.org/rfc/rfc9110#section-7.6.1)
