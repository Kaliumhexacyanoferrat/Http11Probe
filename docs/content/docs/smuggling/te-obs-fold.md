---
title: "TE-OBS-FOLD"
description: "TE-OBS-FOLD test documentation"
weight: 50
---

| | |
|---|---|
| **Test ID** | `SMUG-TE-OBS-FOLD` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §5.1](https://www.rfc-editor.org/rfc/rfc9112#section-5.1) |
| **Requirement** | MUST |
| **Expected** | `400` |

## What it sends

Transfer-Encoding header value wrapped using obs-fold (obsolete line folding), with Content-Length also present.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding:\r\n
 chunked\r\n
Content-Length: 5\r\n
\r\n
hello
```

The Transfer-Encoding value `chunked` is placed on a continuation line (preceded by CRLF and a space), using the obsolete line folding syntax.


## What the RFC says

> "A server that receives an obs-fold in a request message that is not within a message/http container MUST either reject the message by sending a 400 (Bad Request)... or replace each received obs-fold with one or more SP octets prior to interpreting the field value." — RFC 9112 §5.1

When obs-fold is used on the Transfer-Encoding header with Content-Length also present, the risk is acute: a folding-aware parser unfolds the value and sees `Transfer-Encoding: chunked`, while a strict parser that does not recognize the fold sees an empty Transfer-Encoding value and falls back to Content-Length. This creates a direct CL/TE desync.

## Why it matters

This is a high-confidence smuggling vector. The obs-fold mechanism was deprecated precisely because of parser disagreements. When applied to Transfer-Encoding — the header that determines message framing — it creates a situation where one parser uses chunked encoding and another uses Content-Length, enabling request smuggling. The RFC requires rejection (MUST), and no `AllowConnectionClose` alternative is acceptable because the server must actively reject the malformed header rather than simply closing the connection.

## Sources

- [RFC 9112 §5.1](https://www.rfc-editor.org/rfc/rfc9112#section-5.1)
- [RFC 9112 §6.1](https://www.rfc-editor.org/rfc/rfc9112#section-6.1)
