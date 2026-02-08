---
title: "NON-ASCII-HEADER-NAME"
description: "NON-ASCII-HEADER-NAME test documentation"
weight: 9
---

| | |
|---|---|
| **Test ID** | `MAL-NON-ASCII-HEADER-NAME` |
| **Category** | Malformed Input |
| **Expected** | `400` or close |

## What it sends

A request with non-ASCII bytes (`\x80`-`\xFF`) in a header field name.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-T\xC3\xABst: value\r\n
\r\n
```

The header name `X-Tëst` contains UTF-8 encoded `ë` (`\xC3\xAB`) — non-ASCII bytes in a header name.


## What the RFC says

Header field names are tokens (`1*tchar`), and tchar is limited to ASCII. Non-ASCII bytes violate the grammar.

## Sources

- [RFC 9110 Section 5.6.2](https://www.rfc-editor.org/rfc/rfc9110#section-5.6.2)
