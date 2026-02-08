---
title: "METHOD-CONNECT-NO-PORT"
description: "METHOD-CONNECT-NO-PORT test documentation"
weight: 13
---

| | |
|---|---|
| **Test ID** | `COMP-METHOD-CONNECT-NO-PORT` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 Section 3.2.3](https://www.rfc-editor.org/rfc/rfc9112#section-3.2.3) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

`CONNECT example.com HTTP/1.1` — a CONNECT request with authority-form that is missing the required port.

## What the RFC says

> "The 'authority-form' of request-target is only used for CONNECT requests... authority = uri-host ':' port" — RFC 9112 Section 3.2.3

The authority-form grammar requires both host and port separated by a colon. Omitting the port makes the request-target invalid.

## Why it matters

A server or proxy that accepts CONNECT without a port must guess the target port, which can lead to unexpected connections. This is a parsing ambiguity that could be exploited for SSRF or port confusion attacks.

## Sources

- [RFC 9112 Section 3.2.3](https://www.rfc-editor.org/rfc/rfc9112#section-3.2.3)
