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

FROM golang:1.23-alpine AS echo-build
WORKDIR /build
COPY src/Servers/TraefikServer/echo/main.go main.go
RUN go build -o /echo-server main.go

FROM traefik:v3.2
COPY --from=plugin /plugin /plugins-local/src/github.com/jdel/staticresponse/
COPY --from=echo-build /echo-server /usr/local/bin/echo-server
COPY src/Servers/TraefikServer/traefik.yml /etc/traefik/traefik.yml
COPY src/Servers/TraefikServer/dynamic.yml /etc/traefik/dynamic.yml
COPY src/Servers/TraefikServer/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]
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
    echo:
      rule: "Path(`/echo`)"
      entryPoints:
        - web
      service: echo-svc

    catchall:
      rule: "PathPrefix(`/`)"
      entryPoints:
        - web
      middlewares:
        - static-ok
      service: noop@internal

  services:
    echo-svc:
      loadBalancer:
        servers:
          - url: "http://127.0.0.1:9090"

  middlewares:
    static-ok:
      plugin:
        staticresponse:
          statusCode: 200
          body: "OK"
```

## Source — `echo/main.go`

```go
package main

import (
	"fmt"
	"net/http"
	"strings"
)

func main() {
	http.HandleFunc("/echo", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "text/plain")
		var sb strings.Builder
		for name, values := range r.Header {
			for _, v := range values {
				sb.WriteString(fmt.Sprintf("%s: %s\n", name, v))
			}
		}
		fmt.Fprint(w, sb.String())
	})
	http.ListenAndServe(":9090", nil)
}
```

## Source — `entrypoint.sh`

```bash
#!/bin/sh
/usr/local/bin/echo-server &
exec traefik "$@"
```
