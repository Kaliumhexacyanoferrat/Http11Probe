---
title: "CL-EXTRA-LEADING-SP"
description: "CL-EXTRA-LEADING-SP test documentation"
weight: 11
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-EXTRA-LEADING-SP` |
| **Category** | Smuggling (Unscored) |
| **Expected** | `400` (strict) or `2xx` (RFC-compliant) |

## What it sends

`Content-Length:  0` — extra space between colon and value.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length:  5\r\n
\r\n
hello
```

Note the double space after the colon (extra leading OWS).


## Why it's unscored

OWS before the field value is permitted. The server may trim it and process normally.

## Sources

- [RFC 9112 Section 5](https://www.rfc-editor.org/rfc/rfc9112#section-5)
