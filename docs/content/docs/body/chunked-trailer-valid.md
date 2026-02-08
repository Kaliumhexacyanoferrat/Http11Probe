---
title: "CHUNKED-TRAILER-VALID"
description: "CHUNKED-TRAILER-VALID test documentation"
weight: 11
---

| | |
|---|---|
| **Test ID** | `COMP-CHUNKED-TRAILER-VALID` |
| **Category** | Compliance |
| **RFC** | [RFC 9112 §7.1.2](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.2) |
| **Requirement** | MUST accept |
| **Expected** | `2xx` |

## What it sends

A valid chunked POST with a single 5-byte chunk, a zero terminator, and a trailer field (`X-Checksum: abc`) after the final chunk.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r\n
hello\r\n
0\r\n
X-Checksum: abc\r\n
\r\n
```

The trailer section appears between the zero-length terminating chunk and the final empty line.

## What the RFC says

> "A trailer section allows the sender to include additional fields at the end of a chunked message in order to supply metadata that might be dynamically generated while the content is sent." -- RFC 9112 Section 7.1.2

The chunked encoding grammar explicitly includes an optional trailer section:

```
chunked-body = *chunk last-chunk trailer-section CRLF
trailer-section = *( field-line CRLF )
```

Trailer fields are valid metadata that follow the zero-length terminating chunk. A compliant HTTP/1.1 server must be able to parse them, even if it chooses to discard them.

## Why it matters

Trailer fields are used in practice for checksums, signatures, and streaming metadata that cannot be known until the body has been fully generated. A server that rejects a valid chunked body just because it contains a trailer section has an incomplete chunked encoding parser. This can break interoperability with legitimate clients and proxies that use trailers.

## Sources

- [RFC 9112 §7.1.2 -- Chunked Trailer Section](https://www.rfc-editor.org/rfc/rfc9112#section-7.1.2)
- [RFC 9110 Section 6.5 -- Trailer Fields](https://www.rfc-editor.org/rfc/rfc9110#section-6.5)
