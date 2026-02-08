---
title: "METHOD-CONNECT"
description: "METHOD-CONNECT test documentation"
weight: 12
---

| | |
|---|---|
| **Test ID** | `COMP-METHOD-CONNECT` |
| **Category** | Compliance |
| **RFC** | [RFC 9110 Section 9.3.6](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.6) |
| **Requirement** | origin server SHOULD reject |
| **Expected** | `400`, `405`, `501`, or close |

## What it sends

`CONNECT example.com:443 HTTP/1.1` — a CONNECT request sent directly to an origin server (not a proxy).

```http
CONNECT example.com:443 HTTP/1.1\r\n
Host: example.com:443\r\n
\r\n
```


## What the RFC says

> "CONNECT is intended only for use in requests to a proxy... A server that does not act as a tunnel for the requested host and port, or which chooses not to open a TCP connection, MUST respond with an appropriate error status code." — RFC 9110 Section 9.3.6

Origin servers are not proxies. They have no reason to accept CONNECT and establish a TCP tunnel.

## Why it matters

If an origin server accepts CONNECT, it effectively becomes an open proxy. This can be exploited for port scanning internal networks, bypassing firewalls, or pivoting attacks through the server.

## Sources

- [RFC 9110 Section 9.3.6](https://www.rfc-editor.org/rfc/rfc9110#section-9.3.6)
