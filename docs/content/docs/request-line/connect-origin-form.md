---
title: "CONNECT-ORIGIN-FORM"
description: "CONNECT-ORIGIN-FORM test documentation"
weight: 18
---

| | |
|---|---|
| **Test ID** | `COMP-CONNECT-ORIGIN-FORM` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §3.2.3](https://www.rfc-editor.org/rfc/rfc9112#section-3.2.3) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A CONNECT request that uses origin-form (`/path`) instead of the required authority-form (`host:port`).

```http
CONNECT /path HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

## What the RFC says

> "A client sending a CONNECT request MUST send the authority form of request-target." -- RFC 9112 Section 3.2.3

The CONNECT method establishes a tunnel to the destination identified by the request-target. The target must be in authority-form (`host:port`), not origin-form (`/path`). A CONNECT request with `/path` is syntactically invalid because the server cannot determine the tunnel endpoint.

## Why it matters

If a server accepts a CONNECT request with origin-form, it may attempt to establish a tunnel to an unintended destination, or worse, process it as a regular proxied request. This confusion between request forms can be exploited to bypass access controls, reach internal services, or cause Server-Side Request Forgery (SSRF) through a misconfigured proxy.

## Sources

- [RFC 9112 §3.2.3 -- CONNECT](https://www.rfc-editor.org/rfc/rfc9112#section-3.2.3)
- [RFC 9110 Section 9.3.6 -- CONNECT](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.6)
