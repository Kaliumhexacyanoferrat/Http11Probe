---
title: "EXPECT-UNKNOWN"
description: "EXPECT-UNKNOWN test documentation"
weight: 7
---

| | |
|---|---|
| **Test ID** | `COMP-EXPECT-UNKNOWN` |
| **Category** | Compliance |
| **RFC** | [RFC 9110 Section 10.1.1](https://www.rfc-editor.org/rfc/rfc9110#section-10.1.1) |
| **Requirement** | MUST respond with 417 |
| **Expected** | `417`; `2xx` is a warning |

## What it sends

`Expect: 200-ok` — an Expect header with a value the server cannot fulfill.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080\r\n
Expect: 200-ok\r\n
\r\n
```

The `Expect` header contains an unknown expectation value (not `100-continue`).


## What the RFC says

> "A server that receives an Expect field value containing a member other than 100-continue MAY respond with a 417 (Expectation Failed) status code to indicate that the unexpected expectation cannot be met." — RFC 9110 Section 10.1.1

While the RFC uses "MAY", a `417 Expectation Failed` is the semantically correct response for an unrecognized Expect value. Silently ignoring unknown expectations is permissible but less strict.

## Why it matters

The Expect mechanism is a contract between client and server. If a server ignores unknown Expect values, clients cannot rely on the mechanism for future extensions. Returning `417` signals clear rejection of unsupported expectations.

## Sources

- [RFC 9110 Section 10.1.1](https://www.rfc-editor.org/rfc/rfc9110#section-10.1.1)
