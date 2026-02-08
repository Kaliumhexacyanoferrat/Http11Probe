---
title: "ABSOLUTE-URI-HOST-MISMATCH"
description: "ABSOLUTE-URI-HOST-MISMATCH test documentation"
weight: 57
---

| | |
|---|---|
| **Test ID** | `SMUG-ABSOLUTE-URI-HOST-MISMATCH` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §3.2.2](https://www.rfc-editor.org/rfc/rfc9112#section-3.2.2) |
| **Requirement** | MAY |
| **Expected** | `400` or `2xx` |

## What it sends

A GET request using absolute-form URI with a host that differs from the Host header.

```http
GET http://other.example.com/ HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

The request-target uses absolute-form with `other.example.com` while the Host header says `localhost:8080`.


## What the RFC says

> "When an origin server receives a request with an absolute-form of request-target, the origin server MUST ignore the received Host header field (if any) and instead use the host information of the request-target." — RFC 9112 §3.2.2

When a server receives absolute-form, the URI host takes priority over the Host header. However, not all servers support absolute-form, and some may ignore the URI and use the Host header regardless.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (handles the mismatch in some way).

## Why it matters

If a proxy routes requests based on the Host header (`localhost:8080`) but the origin server resolves the target based on the URI host (`other.example.com`), routing confusion occurs. An attacker can use this mismatch to access virtual hosts that should be restricted, bypass access controls, or poison caches for a different domain. This is especially dangerous in reverse proxy configurations where the proxy and origin have different URI-vs-Host precedence rules.

## Sources

- [RFC 9112 §3.2.2](https://www.rfc-editor.org/rfc/rfc9112#section-3.2.2)
- [RFC 9110 §7.2](https://www.rfc-editor.org/rfc/rfc9110#section-7.2)
