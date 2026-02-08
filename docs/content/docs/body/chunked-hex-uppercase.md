---
title: "CHUNKED-HEX-UPPERCASE"
description: "CHUNKED-HEX-UPPERCASE test documentation"
weight: 12
---

| | |
|---|---|
| **Test ID** | `COMP-CHUNKED-HEX-UPPERCASE` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1) |
| **Requirement** | MUST accept |
| **Expected** | `2xx` |

## What it sends

A valid chunked POST where the chunk size is expressed using an uppercase hexadecimal digit: `A` (which equals 10 in decimal), followed by exactly 10 bytes of data.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
A\r\n
helloworld\r\n
0\r\n
\r\n
```

The chunk size `A` is uppercase hex for 10. The chunk data `helloworld` is exactly 10 bytes.

## What the RFC says

> "chunk-size = 1*HEXDIG" -- RFC 9112 Section 7.1

`HEXDIG` is defined as `DIGIT / "A" / "B" / "C" / "D" / "E" / "F"` (case-insensitive per RFC 5234 ABNF conventions). Both `a` and `A` represent the decimal value 10. A compliant chunked parser must accept hex digits in any case.

## Why it matters

While most chunk sizes in practice are small decimal numbers (like `5` or `1a`), the grammar allows any combination of uppercase and lowercase hex digits. A parser that only handles lowercase hex, or only decimal digits, will fail on legitimate chunked bodies. This is a basic interoperability requirement for any HTTP/1.1 implementation.

## Sources

- [RFC 9112 §7.1 -- Chunked Transfer Coding](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
- [RFC 5234 -- ABNF (HEXDIG definition)](https://www.rfc-editor.org/rfc/rfc5234#appendix-B.1)
