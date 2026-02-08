---
title: "MANY-HEADERS"
description: "MANY-HEADERS test documentation"
weight: 6
---

| | |
|---|---|
| **Test ID** | `MAL-MANY-HEADERS` |
| **Category** | Malformed Input |
| **Expected** | `400`, `431`, or close |

## What it sends

A request with 10,000 header fields.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-H-0: value\r\n
X-H-1: value\r\n
X-H-2: value\r\n
... (10,000 headers total) ...\r\n
X-H-9999: value\r\n
\r\n
```


## Why it matters

Servers typically allocate data structures for each header. 10,000 headers can cause excessive memory allocation, hash table collisions, or O(n^2) lookup behavior.

## Sources

- [RFC 6585 Section 5](https://www.rfc-editor.org/rfc/rfc6585#section-5)
