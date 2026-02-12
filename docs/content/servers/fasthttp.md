---
title: "FastHTTP"
toc: false
breadcrumbs: false
---

**Language:** Go · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/FastHttpServer)

## Dockerfile

```dockerfile
FROM golang:1.23-alpine AS build
WORKDIR /src
COPY src/Servers/FastHttpServer/go.mod .
COPY src/Servers/FastHttpServer/main.go .
RUN go mod tidy && CGO_ENABLED=0 go build -o /fasthttp-server .

FROM alpine:3.20
COPY --from=build /fasthttp-server /usr/local/bin/
ENTRYPOINT ["fasthttp-server", "8080"]
```

## Source — `main.go`

```go
package main

import (
	"os"

	"github.com/valyala/fasthttp"
)

func main() {
	port := "8080"
	if len(os.Args) > 1 {
		port = os.Args[1]
	}

	handler := func(ctx *fasthttp.RequestCtx) {
		ctx.SetStatusCode(200)
		if string(ctx.Method()) == "POST" {
			ctx.SetBody(ctx.Request.Body())
			return
		}
		ctx.SetBodyString("OK")
	}

	fasthttp.ListenAndServe("0.0.0.0:"+port, handler)
}
```
