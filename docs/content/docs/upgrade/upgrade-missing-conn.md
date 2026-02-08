---
title: "UPGRADE-MISSING-CONN"
description: "UPGRADE-MISSING-CONN test documentation"
weight: 2
---

| | |
|---|---|
| **Test ID** | `COMP-UPGRADE-MISSING-CONN` |
| **Category** | Compliance |
| **RFC** | [RFC 9110 Section 7.8](https://www.rfc-editor.org/rfc/rfc9110#section-7.8) |
| **Requirement** | MUST NOT switch |
| **Expected** | any non-`101` response |

## What it sends

A GET request with `Upgrade: websocket` and WebSocket handshake headers, but **without** the required `Connection: Upgrade` header.

## What the RFC says

> "A client that sends Upgrade in a request also needs to send 'Connection: Upgrade' to prevent Upgrade from being forwarded to an upstream server." — RFC 9110 Section 7.8

Without `Connection: Upgrade`, the Upgrade header is hop-by-hop metadata that intermediaries should strip. A server that switches protocol anyway could be tricked via proxies.

## Why it matters

If a server switches protocol without `Connection: Upgrade`, a proxy forwarding the request would not know the connection semantics changed. This can lead to connection desync and smuggling through WebSocket-unaware intermediaries.

## Sources

- [RFC 9110 Section 7.8](https://www.rfc-editor.org/rfc/rfc9110#section-7.8)
- [RFC 6455 Section 4.1](https://www.rfc-editor.org/rfc/rfc6455#section-4.1)
