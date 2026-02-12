---
title: "HAProxy"
toc: false
breadcrumbs: false
---

**Language:** C · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/HAProxyServer)

## Dockerfile

```dockerfile
FROM haproxy:3.0-alpine
COPY src/Servers/HAProxyServer/haproxy.cfg /usr/local/etc/haproxy/haproxy.cfg
```

## Source — `haproxy.cfg`

```text
global
    log stdout format raw local0

defaults
    mode http
    timeout client 10s
    timeout connect 5s
    timeout server 10s

frontend http_in
    bind *:8080
    http-request return status 200 content-type "text/plain" string "OK"
```
