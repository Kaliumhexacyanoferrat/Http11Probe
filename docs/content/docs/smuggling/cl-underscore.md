---
title: "CL-UNDERSCORE"
description: "CL-UNDERSCORE test documentation"
weight: 46
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-UNDERSCORE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

Content-Length with an underscore digit separator: `Content-Length: 1_0` with 10 bytes of body.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 1_0\r\n
\r\n
helloworld
```


## What the RFC says

> "Content-Length = 1*DIGIT" — RFC 9110 §8.6

The underscore character is not a digit. The `1*DIGIT` grammar only permits ASCII digits 0-9, so `1_0` is not a valid Content-Length value and MUST be treated as an unrecoverable error.

## Why it matters

Several programming languages (Python, Rust, Java, Ruby, Kotlin) accept underscores as numeric separators in source code (e.g., `1_000_000`). If a server's parser uses a language-level integer-parsing function that accepts underscores, it would read `1_0` as `10`. A stricter front-end proxy would reject the request or misparse the value, creating a parser differential that enables request smuggling.

## Sources

- [RFC 9110 §8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
- [RFC 9112 §6.3](https://www.rfc-editor.org/rfc/rfc9112#section-6.3)
