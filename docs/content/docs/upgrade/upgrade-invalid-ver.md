---
title: "UPGRADE-INVALID-VER"
description: "UPGRADE-INVALID-VER test documentation"
weight: 4
---

| | |
|---|---|
| **Test ID** | `COMP-UPGRADE-INVALID-VER` |
| **Category** | Compliance |
| **RFC** | [RFC 6455 Section 4.4](https://www.rfc-editor.org/rfc/rfc6455#section-4.4) |
| **Requirement** | SHOULD return 426 |
| **Expected** | `426` or non-`101` |

## What it sends

A valid WebSocket upgrade request with `Sec-WebSocket-Version: 99` — a version the server does not support.

## What the RFC says

> "If the requested version is not supported by the server, the server MUST abort the WebSocket handshake... and then send an appropriate HTTP error code (such as 426 Upgrade Required), and a |Sec-WebSocket-Version| header field indicating the version(s) the server is capable of understanding." — RFC 6455 Section 4.4

## Why it matters

A server that returns `101` for an unsupported WebSocket version would enter an undefined protocol state. The ideal response is `426` with a `Sec-WebSocket-Version` header listing supported versions. Servers that don't support WebSocket at all will return 2xx (ignoring the upgrade), which is acceptable but noted as a warning.

## Sources

- [RFC 6455 Section 4.4](https://www.rfc-editor.org/rfc/rfc6455#section-4.4)
