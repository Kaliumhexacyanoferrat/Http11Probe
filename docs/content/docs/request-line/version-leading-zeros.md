---
title: "VERSION-LEADING-ZEROS"
description: "VERSION-LEADING-ZEROS test documentation"
weight: 16
---

| | |
|---|---|
| **Test ID** | `COMP-VERSION-LEADING-ZEROS` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §2.3](https://www.rfc-editor.org/rfc/rfc9112#section-2.3) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A request with `HTTP/01.01` as the version -- leading zeros on both the major and minor version digits.

```http
GET / HTTP/01.01\r\n
Host: localhost:8080\r\n
\r\n
```

## What the RFC says

> "HTTP-version = HTTP-name '/' DIGIT '.' DIGIT" -- RFC 9112 Section 2.3

The grammar specifies exactly one `DIGIT` on each side of the dot. `01` is two digits, not one. `HTTP/01.01` does not match the production rule, making it a syntactically invalid version string.

## Why it matters

Leading zeros may cause version comparison bugs. A parser that strips leading zeros might interpret `HTTP/01.01` as `HTTP/1.1`, while another parser might reject it or treat it as an unknown version. This disagreement between parsers can lead to inconsistent behavior in proxy chains, where one component processes the request differently than another.

## Sources

- [RFC 9112 §2.3 -- HTTP Version](https://www.rfc-editor.org/rfc/rfc9112#section-2.3)
