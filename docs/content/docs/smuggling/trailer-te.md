---
title: "TRAILER-TE"
description: "TRAILER-TE test documentation"
weight: 33
---

| | |
|---|---|
| **Test ID** | `SMUG-TRAILER-TE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 6.5.1](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.1) |
| **Requirement** | MUST ignore |
| **Expected** | `400` preferred; `2xx` is a warning |

## What it sends

A valid chunked request with a `Transfer-Encoding: chunked` header in the trailer section.

## What the RFC says

> "A sender MUST NOT generate a trailer field unless the sender knows the corresponding header field name's definition permits sending it as a trailer field." — RFC 9110 Section 6.5.1

Transfer-Encoding controls message framing and is prohibited in trailers.

## Why it matters

If a server processes the `Transfer-Encoding` trailer, it could attempt to re-decode the already-decoded body or change framing expectations for the next message on the connection. A compliant server should either reject the request or silently discard the prohibited trailer field.

## Sources

- [RFC 9110 Section 6.5.1](https://www.rfc-editor.org/rfc/rfc9110#section-6.5.1)
