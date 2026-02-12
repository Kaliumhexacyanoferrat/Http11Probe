---
title: "Traefik"
toc: false
breadcrumbs: false
---

**Language:** Go · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/TraefikServer)

## Dockerfile

```dockerfile
FROM alpine:3.20 AS plugin
RUN apk add --no-cache git
RUN git clone https://github.com/jdel/staticresponse.git /plugin

FROM traefik:v3.2
COPY --from=plugin /plugin /plugins-local/src/github.com/jdel/staticresponse/
COPY src/Servers/TraefikServer/traefik.yml /etc/traefik/traefik.yml
COPY src/Servers/TraefikServer/dynamic.yml /etc/traefik/dynamic.yml
```

## Source — `traefik.yml`

```yaml
entryPoints:
  web:
    address: ":8080"

providers:
  file:
    filename: /etc/traefik/dynamic.yml

experimental:
  localPlugins:
    staticresponse:
      moduleName: github.com/jdel/staticresponse
```

## Source — `dynamic.yml`

```yaml
http:
  routers:
    catchall:
      rule: "PathPrefix(`/`)"
      entryPoints:
        - web
      middlewares:
        - static-ok
      service: noop@internal

  middlewares:
    static-ok:
      plugin:
        staticresponse:
          statusCode: 200
          body: "OK"
```
