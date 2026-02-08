---
title: "UPGRADE-POST"
description: "UPGRADE-POST test documentation"
weight: 1
---

| | |
|---|---|
| **Test ID** | `COMP-UPGRADE-POST` |
| **Category** | Compliance |
| **RFC** | [RFC 6455 Section 4.1](https://www.rfc-editor.org/rfc/rfc6455#section-4.1) |
| **Requirement** | MUST use GET |
| **Expected** | any non-`101` response |

## What it sends

A WebSocket upgrade request using `POST` instead of `GET`, with all standard WebSocket headers (`Connection: Upgrade`, `Upgrade: websocket`, `Sec-WebSocket-Key`, `Sec-WebSocket-Version: 13`).

## What the RFC says

> "The method of the request MUST be GET, and the HTTP version MUST be at least 1.1." — RFC 6455 Section 4.1

The opening handshake is defined exclusively for GET. A server that accepts an upgrade via POST is violating the WebSocket protocol.

## Why it matters

Allowing WebSocket upgrades on non-GET methods expands the attack surface. POST-based upgrades could bypass CSRF protections or WAF rules that only inspect GET-based WebSocket handshakes.

## Sources

- [RFC 6455 Section 4.1](https://www.rfc-editor.org/rfc/rfc6455#section-4.1)
