---
title: "HEAD-CL-BODY"
description: "HEAD-CL-BODY test documentation"
weight: 35
---

| | |
|---|---|
| **Test ID** | `SMUG-HEAD-CL-BODY` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 9.3.2](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.2) |
| **Requirement** | server must consume or reject body |
| **Expected** | `400` preferred; `2xx` is a warning |

## What it sends

`HEAD / HTTP/1.1` with `Content-Length: 5` and body `hello`. HEAD requests are not supposed to have a response body, but this test sends a request body.

```http
HEAD / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 5\r\n
\r\n
hello
```


## What the RFC says

> "The HEAD method is identical to GET except that the server MUST NOT send content in the response." — RFC 9110 Section 9.3.2

The RFC does not prohibit a request body on HEAD, but the server must properly consume or discard any sent body to prevent it from spilling into the next request on the connection.

## Why it matters

If a server responds to HEAD without reading the `Content-Length` worth of body bytes, those bytes remain on the connection and are interpreted as the start of the next request. This is a connection desync that an attacker can exploit for smuggling on persistent connections.

## Sources

- [RFC 9110 Section 9.3.2](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.2)
