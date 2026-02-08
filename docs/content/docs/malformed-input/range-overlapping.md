---
title: "RANGE-OVERLAPPING"
description: "RANGE-OVERLAPPING test documentation"
weight: 25
---

| | |
|---|---|
| **Test ID** | `MAL-RANGE-OVERLAPPING` (unscored) |
| **Category** | Malformed Input |
| **RFC** | [RFC 9110 Section 14.2](https://www.rfc-editor.org/rfc/rfc9110#section-14.2) |
| **Expected** | Any response = Warn |

## What it sends

A GET request with a Range header containing 1,000 overlapping range values.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
Range: bytes=0-,0-,0-,...{1000 total}...\r\n
\r\n
```

The `Range` header contains 1,000 repetitions of `0-`, each requesting the entire resource.

## Why it matters

CVE-2011-3192 (Apache Range header DoS) showed that servers that expand each range independently can consume massive memory. A single request with many overlapping ranges could cause the server to generate a multipart response containing thousands of copies of the same content, exhausting memory and CPU. A robust server should either ignore the Range header, merge overlapping ranges, or reject the request.

## Sources

- [RFC 9110 Section 14.2 -- Range](https://www.rfc-editor.org/rfc/rfc9110#section-14.2)
- [CVE-2011-3192 -- Apache Range header DoS](https://nvd.nist.gov/vuln/detail/CVE-2011-3192)
