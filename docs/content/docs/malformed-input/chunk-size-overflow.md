---
title: "CHUNK-SIZE-OVERFLOW"
description: "CHUNK-SIZE-OVERFLOW test documentation"
weight: 16
---

| | |
|---|---|
| **Test ID** | `MAL-CHUNK-SIZE-OVERFLOW` |
| **Category** | Malformed Input |
| **Expected** | `400` or close |

## What it sends

A chunked request with a chunk size of `FFFFFFFFFFFFFFFF0` — a value exceeding the maximum 64-bit unsigned integer.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
FFFFFFFFFFFFFFFFFF0\r\n
hello\r\n
0\r\n
\r\n
```

The chunk size `FFFFFFFFFFFFFFFFFF0` exceeds 64-bit hex integer range.


## Why it matters

Integer overflow in chunk size parsing can lead to incorrect body length calculation, buffer overflows, or server crashes. A robust server must detect overflow and reject the request.

## Sources

- RFC 9112 Section 7.1 — chunk-size = 1*HEXDIG
