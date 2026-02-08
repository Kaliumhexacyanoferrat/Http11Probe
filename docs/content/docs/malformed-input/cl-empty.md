---
title: "CL-EMPTY"
description: "CL-EMPTY test documentation"
weight: 19
---

| | |
|---|---|
| **Test ID** | `MAL-CL-EMPTY` |
| **Category** | Malformed Input |
| **RFC** | [RFC 9110 Section 8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

`Content-Length: ` — a Content-Length header with an empty value (just whitespace after the colon).

## What the RFC says

> "Content-Length = 1*DIGIT" — RFC 9110 Section 8.6

The grammar requires at least one digit. An empty value is not a valid Content-Length and indicates invalid message framing.

## Why it matters

Parsers that treat an empty Content-Length as `0` will read no body, while others may reject it or wait for data. This disagreement between parsers can be exploited for smuggling when the request also carries a body.

## Sources

- [RFC 9110 Section 8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
