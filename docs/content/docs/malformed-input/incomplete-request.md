---
title: "INCOMPLETE-REQUEST"
description: "INCOMPLETE-REQUEST test documentation"
weight: 12
---

| | |
|---|---|
| **Test ID** | `MAL-INCOMPLETE-REQUEST` |
| **Category** | Malformed Input |
| **Expected** | `400`, close, or timeout |

## What it sends

A partial HTTP request -- the request-line and some headers, but the connection is closed before the final CRLF.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
X-Test: value
```

The request ends abruptly after the `X-Test` header value — no `\r\n` line terminator and no blank line to signal end of headers.


## Why timeout is acceptable

The server may be waiting for the rest of the headers. It has received a valid prefix but not a complete request.

## Sources

- No specific RFC section -- this is a robustness test
