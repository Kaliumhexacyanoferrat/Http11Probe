---
title: "OPTIONS-CL-BODY"
description: "OPTIONS-CL-BODY test documentation"
weight: 36
---

| | |
|---|---|
| **Test ID** | `SMUG-OPTIONS-CL-BODY` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 9.3.7](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.7) |
| **Requirement** | server should consume or reject body |
| **Expected** | `400` preferred; `2xx` is a warning |

## What it sends

`OPTIONS / HTTP/1.1` with `Content-Length: 5` and body `hello`.

```http
OPTIONS / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 5\r\n
\r\n
hello
```


## What the RFC says

> "A client that generates an OPTIONS request containing content MUST send a valid Content-Type header field describing the representation media type." — RFC 9110 Section 9.3.7

OPTIONS requests may have a body, but the server must properly handle it. If the body is not consumed, it leaks onto the connection.

## Why it matters

Like HEAD with a body, if the server responds to OPTIONS without reading the declared body bytes, the remaining data is misinterpreted as the next request. This desync can be exploited to smuggle requests, especially when OPTIONS is commonly used for CORS preflight.

## Sources

- [RFC 9110 Section 9.3.7](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.7)
