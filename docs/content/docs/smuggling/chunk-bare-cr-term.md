---
title: "CHUNK-BARE-CR-TERM"
description: "CHUNK-BARE-CR-TERM test documentation"
weight: 53
---

| | |
|---|---|
| **Test ID** | `SMUG-CHUNK-BARE-CR-TERM` |
| **Category** | Smuggling |
| **RFC** | [RFC 9112 §2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A chunked request where the chunk size line is terminated by bare CR (`\r`) without LF.

```http
POST / HTTP/1.1\r\n
Host: localhost:8080\r\n
Transfer-Encoding: chunked\r\n
\r\n
5\r
hello\r\n
0\r\n
\r\n
```

The chunk size `5` is followed by a bare CR (`\r`) instead of the required CRLF (`\r\n`). The bytes `hello` immediately follow the bare CR.


## What the RFC says

> "Although the line terminator for the start-line and fields is the sequence CRLF, a recipient MAY recognize a single LF as a line terminator and ignore any preceding CR." — RFC 9112 §2.2

The RFC explicitly permits MAY-accept for bare LF, but makes no such allowance for bare CR. CRLF is the only valid line terminator, and bare CR alone is not a recognized alternative. A bare CR without a following LF must be rejected.

## Why it matters

Some parsers treat CR alone as a line ending (a behavior inherited from old Mac-style line endings). If one parser accepts bare CR as a chunk-size terminator and another requires CRLF, they disagree on where the chunk data begins. The strict parser sees `5\rhello` as a malformed chunk size (containing `\r`, `h`, `e`, `l`, `l`, `o`), while the lenient parser sees chunk size 5 followed by `hello` as chunk data. This boundary disagreement enables chunk-level desynchronization.

## Sources

- [RFC 9112 §2.2](https://www.rfc-editor.org/rfc/rfc9112#section-2.2)
- [RFC 9112 §7.1](https://www.rfc-editor.org/rfc/rfc9112#section-7.1)
