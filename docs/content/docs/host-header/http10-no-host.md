---
title: "HTTP10-NO-HOST"
description: "HTTP10-NO-HOST test documentation"
weight: 7
---

| | |
|---|---|
| **Test ID** | `COMP-HTTP10-NO-HOST` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §3.2](https://www.rfc-editor.org/rfc/rfc9112#section-3.2) |
| **Requirement** | MAY (unscored) |
| **Expected** | `200` = Warn, `400` = Pass |

## What it sends

An HTTP/1.0 request with no `Host` header at all.

```http
GET / HTTP/1.0\r\n
\r\n
```

No `Host` header is present, and the HTTP version is 1.0.

## What the RFC says

The `Host` header requirement was introduced in HTTP/1.1. HTTP/1.0 predates this requirement, so an HTTP/1.0 request without a `Host` header is not technically a protocol violation:

> "A server MUST respond with a 400 (Bad Request) status code to any HTTP/1.1 request message that lacks a Host header field..." -- RFC 9112 Section 3.2

Note the requirement explicitly says "HTTP/1.1 request message." For HTTP/1.0, the server may choose to accept the request (routing to a default virtual host) or reject it.

**Pass:** Server rejects with `400` (strict -- good security practice).
**Warn:** Server accepts with `200` (valid -- HTTP/1.0 did not require Host).

## Why it matters

In a virtual hosting environment, a request without a `Host` header gives the server no indication of which site is being targeted. Accepting such requests means the server must fall back to a default host, which could serve unintended content. Rejecting HTTP/1.0 requests without `Host` is the safer approach, especially since legitimate modern clients always send a `Host` header regardless of HTTP version.

## Sources

- [RFC 9112 §3.2 -- Request Target](https://www.rfc-editor.org/rfc/rfc9112#section-3.2)
- [RFC 9110 Section 7.2 -- Host and :authority](https://www.rfc-editor.org/rfc/rfc9110#section-7.2)
