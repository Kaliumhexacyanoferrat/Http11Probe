---
title: "CL-INTERNAL-SPACE"
description: "CL-INTERNAL-SPACE test documentation"
weight: 27
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-INTERNAL-SPACE` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 Section 8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6) |
| **Requirement** | MUST reject |
| **Expected** | `400` or close |

## What it sends

`Content-Length: 1 0` — space inside the number.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 1 0\r\n
\r\n
hello12345
```

The Content-Length value `1 0` has a space between the digits.


## What the RFC says

> CL grammar is `1*DIGIT` with no internal whitespace.

## Why it matters

A server that interprets `1 0` as 10 reads more data than one that reads only 1 byte or rejects.

## Sources

- [RFC 9110 Section 8.6](https://www.rfc-editor.org/rfc/rfc9110#section-8.6)
