---
title: "TRAILER-HOST"
description: "TRAILER-HOST test documentation"
weight: 34
---

| | |
|---|---|
| **Test ID** | `SMUG-TRAILER-HOST` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 6.5.2](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.2) |
| **Requirement** | MUST NOT use for routing |
| **Expected** | `400` preferred; `2xx` is a warning |

## What it sends

A valid chunked request with `Host: evil.example.com` in the trailer section, while the actual Host header in the request points to the real server.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello\r\n
0\r\n
Host: evil.example.com\r\n
\r\n
```

A `Host: evil.example.com` header appears in the chunked trailers section.


## What the RFC says

> "Field names that define constraints on the message framing or that could alter the semantics of the message content... MUST NOT be generated as a trailer field." — RFC 9110 Section 6.5.2

Host is a request-routing field that controls which virtual host processes the request. It must not appear in trailers.

## Why it matters

If a server or middleware reads the Host trailer and uses it for routing decisions, an attacker could redirect requests to a different virtual host or backend after the message body has already been accepted. This is a form of late-binding host injection.

## Sources

- [RFC 9110 Section 6.5.2](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.2)
