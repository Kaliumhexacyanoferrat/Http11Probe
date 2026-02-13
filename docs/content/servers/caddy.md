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
COPY src/Servers/CaddyServer/echo.html /srv/echo.html
```

## Source — `Caddyfile`

```text
:8080 {
    handle /echo {
        root * /srv
        templates {
            mime text/plain
        }
        rewrite * /echo.html
        file_server
    }
    respond "OK" 200
}
```

## Source — `echo.html`

```html
{{range $key, $vals := .Req.Header}}{{range $vals}}{{$key}}: {{.}}
{{end}}{{end}}
```
