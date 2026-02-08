---
title: "UPGRADE-UNKNOWN"
description: "UPGRADE-UNKNOWN test documentation"
weight: 3
---

| | |
|---|---|
| **Test ID** | `COMP-UPGRADE-UNKNOWN` |
| **Category** | Compliance |
| **RFC** | [RFC 9110 Section 7.8](https://www.rfc-editor.org/rfc/rfc9110#section-7.8) |
| **Requirement** | MUST NOT switch |
| **Expected** | any non-`101` response |

## What it sends

A GET request with `Connection: Upgrade` and `Upgrade: totally-made-up/1.0` — a protocol the server cannot possibly support.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
Connection: Upgrade\r\n
Upgrade: totally-made-up/1.0\r\n
\r\n
```


## What the RFC says

> "A server MUST NOT switch to a protocol that was not indicated by the client in the corresponding request's Upgrade header field." — RFC 9110 Section 7.8

More importantly, a server must not respond with `101 Switching Protocols` for a protocol it doesn't implement. It should ignore the Upgrade header and process the request normally (2xx) or reject it.

## Why it matters

A server that blindly returns `101` for any Upgrade value is broken — it switches the connection to an undefined state, potentially leaving the TCP stream in a desynced state exploitable for smuggling.

## Sources

- [RFC 9110 Section 7.8](https://www.rfc-editor.org/rfc/rfc9110#section-7.8)
