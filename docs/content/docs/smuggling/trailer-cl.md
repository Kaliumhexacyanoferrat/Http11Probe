---
title: "TRAILER-CL"
description: "TRAILER-CL test documentation"
weight: 32
---

| | |
|---|---|
| **Test ID** | `SMUG-TRAILER-CL` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 6.5.1](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.1) |
| **Requirement** | MUST ignore |
| **Expected** | `400` preferred; `2xx` is a warning |

## What it sends

A valid chunked request with a `Content-Length: 50` header in the trailer section (after the last chunk).

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello\r\n
0\r\n
Content-Length: 50\r\n
\r\n
```

A `Content-Length: 50` header appears in the chunked trailers section.


## What the RFC says

> "A sender MUST NOT generate a trailer field unless the sender knows the corresponding header field name's definition permits sending it as a trailer field." — RFC 9110 Section 6.5.1

Content-Length is explicitly listed as a field that "MUST NOT be sent in a trailer section" because it controls message framing.

## Why it matters

If a server or proxy processes the `Content-Length` trailer, it could retroactively change its understanding of the message body length — potentially poisoning a cache or re-framing subsequent requests on the same connection.

## Sources

- [RFC 9110 Section 6.5.1](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.1)
- [RFC 9110 Section 6.5.2](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.2)
