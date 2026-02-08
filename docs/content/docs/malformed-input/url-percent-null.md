---
title: "URL-PERCENT-NULL"
description: "URL-PERCENT-NULL test documentation"
weight: 23
---

| | |
|---|---|
| **Test ID** | `MAL-URL-PERCENT-NULL` |
| **Category** | Malformed Input |
| **Expected** | `400` = Pass, `2xx`/`404` = Warn |

## What it sends

A GET request with a percent-encoded NUL byte (`%00`) in the URL path.

```http
GET /path%00.html HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

## Why it matters

Percent-encoded NUL byte (`%00`) can cause C-based servers to truncate the path string at the null byte. For example, `file%00.php` might be interpreted as `file` while bypassing extension-based access controls. An attacker could use this to access files that should be restricted by file extension checks, or to upload a malicious file with a double extension like `shell.php%00.jpg`.

## Sources

- [CWE-158 -- Improper Neutralization of Null Byte or NUL Character](https://cwe.mitre.org/data/definitions/158.html)
