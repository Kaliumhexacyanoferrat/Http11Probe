---
title: "CL-TRAILING-SPACE"
description: "CL-TRAILING-SPACE test documentation"
weight: 10
---

| | |
|---|---|
| **Test ID** | `SMUG-CL-TRAILING-SPACE` |
| **Category** | Smuggling (Unscored) |
| **Expected** | `400` (strict) or `2xx` (RFC-compliant) |

## What it sends

`Content-Length: 0 ` — trailing space after the value.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Content-Length: 5 \r\n
\r\n
hello
```

Note the trailing space after `5` in the Content-Length value.


## Why it's unscored

OWS (optional whitespace) after the field value is explicitly permitted by RFC 9112 Section 5. Trimming it and processing normally is valid behavior. However, `400` is the stricter/safer choice.

## Sources

- [RFC 9112 Section 5](https://www.rfc-editor.org/rfc/rfc9112#section-5)
