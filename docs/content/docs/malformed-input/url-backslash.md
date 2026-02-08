---
title: "URL-BACKSLASH"
description: "URL-BACKSLASH test documentation"
weight: 21
---

| | |
|---|---|
| **Test ID** | `MAL-URL-BACKSLASH` |
| **Category** | Malformed Input |
| **Expected** | `400` = Pass, `2xx`/`404` = Warn |

## What it sends

A GET request with a backslash in the URL path.

```http
GET /path\file HTTP/1.1\r\n
Host: localhost:8080\r\n
\r\n
```

## Why it matters

Backslash is not a valid URI character per RFC 3986. Some servers (especially on Windows) normalize `\` to `/`, which can enable path traversal attacks if used to bypass URL filters. For example, a WAF blocking `../` might not block `..\`, allowing an attacker to traverse directories on servers that treat backslash as a path separator.

## Sources

- [RFC 3986 — Uniform Resource Identifier (URI): Generic Syntax](https://www.rfc-editor.org/rfc/rfc3986)
