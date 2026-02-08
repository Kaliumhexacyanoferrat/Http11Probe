---
title: "MULTIPLE-HOST-COMMA"
description: "MULTIPLE-HOST-COMMA test documentation"
weight: 52
---

| | |
|---|---|
| **Test ID** | `SMUG-MULTIPLE-HOST-COMMA` |
| **Category** | Smuggling |
| **RFC** | [RFC 9110 §7.2](https://www.rfc-editor.org/rfc/rfc9110#section-7.2) |
| **Requirement** | MUST |
| **Expected** | `400` or close |

## What it sends

A Host header with two comma-separated hostnames.

```http
GET / HTTP/1.1\r\n
Host: localhost:8080, other.example.com\r\n
\r\n
```

The Host header contains `localhost:8080, other.example.com` — two distinct hostnames in a single header value.


## What the RFC says

> "A server MUST respond with a 400 (Bad Request) status code to any HTTP/1.1 request message that lacks a Host header field and to any request message that contains more than one Host header field line or a Host header field with an invalid field-value." — RFC 9110 §7.2

The Host header is not a list-based field. A comma in the Host value does not indicate multiple list elements — it means the value itself contains two distinct hostnames, which is invalid. The server MUST reject such a request.

## Why it matters

If a front-end proxy extracts the first hostname (`localhost:8080`) for routing but the back-end extracts the second (`other.example.com`), routing confusion occurs. An attacker could use this to bypass virtual host restrictions, access internal services, or poison caches for the wrong host. This is a host-header injection vector that enables cache poisoning and SSRF attacks.

## Sources

- [RFC 9110 §7.2](https://www.rfc-editor.org/rfc/rfc9110#section-7.2)
