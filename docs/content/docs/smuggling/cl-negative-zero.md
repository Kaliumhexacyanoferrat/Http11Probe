---
title: "CL-NEGATIVE-ZERO"
description: "CL-NEGATIVE-ZERO test documentation"
weight: 47
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-NEGATIVE-ZERO` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

Content-Length with a negative zero value: `Content-Length: -0`.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: -0\r\n
\r\n
```


## What the RFC says

> "Content-Length = 1*DIGIT" — RFC 9110 §8.6

The `1*DIGIT` grammar means only one or more ASCII digits (0-9) are permitted. The minus sign (`-`) is not a digit, so `-0` is invalid regardless of the fact that -0 equals 0 mathematically. An invalid Content-Length MUST be treated as an unrecoverable error.

## Why it matters

Some parsers apply numeric conversion first and check validity second. If a parser converts `-0` to the integer `0` and accepts it, it silently consumes an invalid format. A stricter front-end might reject the request or see no body at all, while a lenient back-end accepts it — creating framing disagreement. The `-` character is especially dangerous because it could allow negative body lengths through similar parser shortcuts.

## Sources

- [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
- [RFC 9112 §6.3](https://www.rfc-editor.org/rfc/rfc9112#section-6.3)
