---
title: "CL-TAB-BEFORE-VALUE"
description: "CL-TAB-BEFORE-VALUE test documentation"
weight: 20
---

| | |
|---|---|
| **Test ID** | `MAL-CL-TAB-BEFORE-VALUE` |
| **Category** | Malformed Input |
| **RFC** | [RFC 9110 Section 5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5) |
| **Requirement** | valid per RFC |
| **Expected** | `400` preferred; `2xx` is a warning |

## What it sends

`Content-Length:\t5` — a Content-Length header where a horizontal tab character separates the colon from the value, instead of a space.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length:\t5\r\n
\r\n
hello
```

A horizontal tab character (`\t` / `0x09`) separates the colon from the value instead of a space.


## What the RFC says

> "OWS = *( SP / HTAB )" — RFC 9110 Section 5.6.3

The optional whitespace (OWS) between the colon and the field value may be either spaces or horizontal tabs. A tab character is technically valid per the grammar.

## Why it matters

While tabs are valid OWS, they are rarely used in practice. Some parsers may not handle tab characters correctly — for example, treating the tab as part of the value rather than whitespace, resulting in a failed integer parse or a different numeric interpretation. This edge case tests parser robustness.

## Sources

- [RFC 9110 Section 5.5](https://www.rfc-editor.org/rfc/rfc9110#section-5.5)
- [RFC 9110 Section 5.6.3](https://www.rfc-editor.org/rfc/rfc9110#section-5.6.3)
