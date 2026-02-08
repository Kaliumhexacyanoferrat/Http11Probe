---
title: "METHOD-TRACE"
description: "METHOD-TRACE test documentation"
weight: 14
---

| | |
|---|---|
| **Test ID** | `COMP-METHOD-TRACE` |
| **Category** | Compliance |
| **RFC** | [RFC 9110 Section 9.3.8](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.8) |
| **Requirement** | SHOULD disable |
| **Expected** | `405` or `501` preferred; `200` is a warning |

## What it sends

`TRACE / HTTP/1.1` — a standard TRACE request.

```http
TRACE / HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```


## What the RFC says

> "A client MUST NOT generate header fields in a TRACE request containing sensitive data... A server SHOULD exclude any request header fields received in that request message from the response body..." — RFC 9110 Section 9.3.8

TRACE echoes the received request back to the client. While valid per the HTTP spec, it is widely considered a security risk in production.

## Why it matters

TRACE can be abused for **Cross-Site Tracing (XST)** attacks — if an attacker can trigger a TRACE request (via XSS or other means), the echoed response may expose cookies, authorization headers, or other sensitive data that `HttpOnly` flags are meant to protect.

Most security hardening guides recommend disabling TRACE entirely. A `405 Method Not Allowed` or `501 Not Implemented` response is ideal.

## Sources

- [RFC 9110 Section 9.3.8](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.8)
- [OWASP: Test HTTP Methods](https://owasp.org/www-project-web-security-testing-guide/latest/4-Web_Application_Security_Testing/02-Configuration_and_Deployment_Management_Testing/06-Test_HTTP_Methods)
