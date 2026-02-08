---
title: "TECL-PIPELINE"
description: "TECL-PIPELINE test documentation"
weight: 9
---

| | |
|---|---|
| **Test ID** | `SMUG-TECL-PIPELINE` |
| **Category** | Smuggling |
| **Expected** | `400` or close |

## What it sends

A full TE.CL smuggling payload — the reverse of CLTE. The front-end uses Transfer-Encoding and the body is crafted so the back-end (using Content-Length) sees a smuggled request.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
Content-Length: 30\r\n
\r\n
0\r\n
\r\n
```

Followed immediately on the same connection by:

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

A TE parser sees the `0` chunk as end-of-body. A CL-only parser tries to read 30 bytes and consumes the follow-up `GET` as body data.


## Why it matters

The TE.CL variant is equally dangerous to CL.TE. Together, they cover both possible orderings of front-end/back-end preference.

## Sources

- [RFC 9112 Section 6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
- [PortSwigger — HTTP Request Smuggling](https://portswigger.net/web-security/request-smuggling)
