---
title: "URL-OVERLONG-UTF8"
description: "URL-OVERLONG-UTF8 test documentation"
weight: 22
---

| | |
|---|---|
| **Test ID** | `MAL-URL-OVERLONG-UTF8` |
| **Category** | Malformed Input |
| **Expected** | `400` or close |

## What it sends

A GET request with raw overlong UTF-8 bytes in the URL path. The bytes `0xC0 0xAF` are an overlong encoding of `/` (U+002F).

```http
GET /\xC0\xAF HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

The two bytes after `/` are `0xC0 0xAF` -- an illegal two-byte UTF-8 sequence that decodes to the ASCII forward slash character.

## Why it matters

Overlong UTF-8 sequences encode characters using more bytes than necessary. `0xC0 0xAF` is an overlong encoding of `/`. If a server decodes this as `/` during path resolution, it can bypass path traversal filters (e.g., `..%c0%af..` becomes `../../`). This was the basis of several IIS vulnerabilities including the infamous IIS Unicode directory traversal exploit. A compliant server must reject overlong UTF-8 sequences as invalid input.

## Sources

- [RFC 3629 -- UTF-8, a transformation format of ISO 10646](https://www.rfc-editor.org/rfc/rfc3629)
- [CVE-2000-0884 -- IIS Unicode directory traversal](https://nvd.nist.gov/vuln/detail/CVE-2000-0884)
