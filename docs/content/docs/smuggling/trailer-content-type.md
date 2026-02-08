---
title: "TRAILER-CONTENT-TYPE"
description: "TRAILER-CONTENT-TYPE test documentation"
weight: 58
---

| | |
|---|---|
| **Test ID** | `SMUG-TRAILER-CONTENT-TYPE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §6.5.1](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.1) |
| **Requirement** | SHOULD ignore |
| **Expected** | `400` or `2xx` |

## What it sends

A valid chunked request with a `Content-Type: text/evil` header in the trailer section (after the last chunk).

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello\r\n
0\r\n
Content-Type: text/evil\r\n
\r\n
```

A `Content-Type: text/evil` header appears in the chunked trailers section.


## What the RFC says

> "A sender MUST NOT generate a trailer field unless the sender knows the corresponding header field name's definition permits sending it as a trailer field." — RFC 9110 §6.5.1

Content-Type is listed as a field that controls message payload interpretation and MUST NOT be sent in a trailer section. A server should either reject the request or silently discard the prohibited trailer field.

**Pass:** Server rejects with `400` (strict, safe).
**Warn:** Server accepts and responds `2xx` (processes body and discards prohibited trailer).

## Why it matters

If a server or middleware processes the `Content-Type` trailer, it could retroactively change how the already-received body is interpreted. An attacker could send a benign `Content-Type` in the headers to pass WAF inspection, then inject a different `Content-Type` in the trailer to trick downstream processors into interpreting the body differently — for example, changing `application/json` to `text/xml` to trigger different parsing paths or bypass content-type-based security filters.

## Sources

- [RFC 9110 §6.5.1](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.1)
- [RFC 9110 §6.5.2](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.2)
