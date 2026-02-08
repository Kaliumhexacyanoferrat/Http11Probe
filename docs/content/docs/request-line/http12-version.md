---
title: "HTTP12-VERSION"
description: "HTTP12-VERSION test documentation"
weight: 19
---

| | |
|---|---|
| **Test ID** | `COMP-HTTP12-VERSION` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §2.3](https://www.rfc-editor.org/rfc/rfc9112#section-2.3) |
| **Requirement** | MAY (unscored) |
| **Expected** | `200` or `505` = Warn |

## What it sends

A request using HTTP version 1.2, which does not exist but has a higher minor version than 1.1.

```http
GET / HTTP/1.2\r\n
Host: localhost:8080\r\n
\r\n
```

## What the RFC says

> "A recipient that receives a message with a major version number that it implements and a minor version number higher than what it implements SHOULD process the message as if it were in the highest minor version within that major version to which the recipient is conformant." -- RFC 9112 Section 2.3

A server implementing HTTP/1.1 that receives `HTTP/1.2` should treat it as HTTP/1.1 and process normally. The server may also respond with `505 HTTP Version Not Supported` if it chooses not to handle unrecognized minor versions.

**Warn:** Server responds `200` (correctly processes as HTTP/1.1) or `505` (refuses the minor version). Both are acceptable behaviors.

## Why it matters

Forward compatibility is a core design principle of HTTP versioning. Minor version increments within the same major version should not break communication. A server that rejects `HTTP/1.2` with a `400` instead of processing it as `HTTP/1.1` or returning `505` has an overly strict version parser that may break when clients or proxies use future HTTP/1.x versions.

## Sources

- [RFC 9112 §2.3 -- HTTP Version](https://www.rfc-editor.org/rfc/rfc9112#section-2.3)
- [RFC 9110 Section 15.6.6 -- 505 HTTP Version Not Supported](https://www.rfc-editor.org/rfc/rfc9110#section-15.6.6)
