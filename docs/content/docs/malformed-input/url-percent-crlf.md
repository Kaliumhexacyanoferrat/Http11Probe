---
title: "URL-PERCENT-CRLF"
description: "URL-PERCENT-CRLF test documentation"
weight: 24
---

| | |
|---|---|
| **Test ID** | `MAL-URL-PERCENT-CRLF` |
| **Category** | Malformed Input |
| **Expected** | `400` = Pass, `2xx`/`404` = Warn |

## What it sends

A GET request with percent-encoded CRLF characters (`%0d%0a`) in the URL, followed by an injected header.

```http
GET /path%0d%0aX-Injected:%20true HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

## Why it matters

Percent-encoded CRLF (`%0d%0a`) in the URL is a header injection vector if the server percent-decodes during initial request parsing. This could allow injecting arbitrary HTTP headers or splitting the response. An attacker could use CRLF injection to set cookies, redirect users, inject content, or poison caches by controlling response headers.

## Sources

- [CWE-113 -- Improper Neutralization of CRLF Sequences in HTTP Headers](https://cwe.mitre.org/data/definitions/113.html)
