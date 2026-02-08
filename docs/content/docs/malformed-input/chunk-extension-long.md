---
title: "CHUNK-EXT-64K"
description: "CHUNK-EXT-64K test documentation"
weight: 18
---

| | |
|---|---|
| **Test ID** | `MAL-CHUNK-EXT-64K` |
| **Category** | Malformed Input |
| **Expected** | `400`/`431` or close |

## What it sends

A chunked request with a chunk extension containing 64KB (65,536 bytes) of data.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5;ext=aaaa...{65,536 x 'a'}...\r\n
hello\r\n
0\r\n
\r\n
```

The chunk extension value is 65,536 bytes of `a` characters.


## Why it matters

While chunk extensions are syntactically valid per RFC 9112 Section 7.1.1, a 64KB extension is pathological. A robust server should reject unreasonably large chunk extensions to prevent resource exhaustion and denial of service. CVE-2023-39326 demonstrated that Go's `net/http` library could be exploited via large chunk extensions to cause excessive memory consumption and DoS.

## Sources

- [RFC 9112 Section 7.1.1 — chunk extensions](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.1)
- [CVE-2023-39326 — Go net/http chunk extension DoS](https://nvd.nist.gov/vuln/detail/CVE-2023-39326)
