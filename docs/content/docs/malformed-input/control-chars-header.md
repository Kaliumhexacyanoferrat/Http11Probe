---
title: "CONTROL-CHARS-HEADER"
description: "CONTROL-CHARS-HEADER test documentation"
weight: 8
---

| | |
|---|---|
| **Test ID** | `MAL-CONTROL-CHARS-HEADER` |
| **Category** | Malformed Input |
| **RFC** | [RFC 9110 Section 5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5) |
| **Expected** | `400` or close |

## What it sends

A request with control characters (`\x01`-`\x08`, `\x0E`-`\x1F`) in a header field value.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-Test: abc\x07\x08\x0Bdef\r\n
\r\n
```

The header value contains BEL (`\x07`), BS (`\x08`), and VT (`\x0B`) control characters.


## What the RFC says

RFC 9110 Section 5.5 defines allowed characters in field values. Control characters other than HTAB are not included.

## Sources

- [RFC 9110 Section 5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5)
