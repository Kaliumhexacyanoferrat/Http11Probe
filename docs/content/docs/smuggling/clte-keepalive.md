---
title: "CLTE-KEEPALIVE"
description: "CLTE-KEEPALIVE sequence test documentation"
weight: 12
---

| | |
|---|---|
| **Test ID** | `SMUG-CLTE-KEEPALIVE` |
| **Category** | Smuggling |
| **Type** | Sequence (2 steps) |
| **Scored** | Yes |
| **RFC** | [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1) |
| **RFC Level** | MUST |
| **Expected** | `400`, or `2xx` + connection close |

## What it does

This is a **sequence test** that verifies the MUST-close requirement still applies even when the client explicitly requests a persistent connection.

### Step 1: Ambiguous POST with keep-alive

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Connection: keep-alive\r\n
Content-Length: 5\r\n
Transfer-Encoding: chunked\r\n
\r\n
0\r\n
\r\n
```

A POST with both `Content-Length: 5` and `Transfer-Encoding: chunked`, plus an explicit `Connection: keep-alive` header pressuring the server to maintain the connection.

### Step 2: Follow-up GET

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

A normal GET sent on the same connection. This step only executes if the connection is still open after step 1.

## What the RFC says

> "**Regardless, the server MUST close the connection after responding to such a request** to avoid the potential attacks." — RFC 9112 §6.1

The word "regardless" means the MUST-close requirement overrides any `Connection: keep-alive` request from the client. The server has no choice — it must close.

## Why it matters

This is the most tempting edge case for servers to get wrong. A server that correctly detects the CL+TE conflict might still honor the client's `keep-alive` request instead of closing. This test specifically targets that logic path.

## Verdicts

- **Pass** — Server returns `400` (rejected outright), OR returns `2xx` and closes the connection despite `keep-alive` (step 2 never executes)
- **Fail** — Server returns `2xx` and honors `keep-alive`, keeping the connection open (step 2 executes and gets a response)

## Sources

- [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
- [RFC 9112 §9.3](https://www.rfc-editor.org/rfc/rfc9112#section-9.3)
