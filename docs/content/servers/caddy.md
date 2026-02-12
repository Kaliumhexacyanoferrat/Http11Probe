---
title: "Caddy"
toc: false
breadcrumbs: false
---

**Language:** Go · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/CaddyServer)

## Dockerfile

```dockerfile
FROM caddy:2
COPY src/Servers/CaddyServer/Caddyfile /etc/caddy/Caddyfile
```

## Source — `Caddyfile`

```text
:8080 {
    respond "OK" 200
}
```
