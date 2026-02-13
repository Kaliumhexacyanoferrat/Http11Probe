---
title: "Gin"
toc: false
breadcrumbs: false
---

**Language:** Go · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/GinServer)

## Dockerfile

```dockerfile
FROM golang:1.23-alpine AS build
WORKDIR /src
COPY src/Servers/GinServer/go.mod .
COPY src/Servers/GinServer/main.go .
RUN go mod tidy && CGO_ENABLED=0 go build -o /gin-server .

FROM alpine:3.20
COPY --from=build /gin-server /usr/local/bin/
ENTRYPOINT ["gin-server", "8080"]
```

## Source — `main.go`

```go
package main

import (
	"io"
	"os"
	"strings"

	"github.com/gin-gonic/gin"
)

func main() {
	port := "8080"
	if len(os.Args) > 1 {
		port = os.Args[1]
	}

	gin.SetMode(gin.ReleaseMode)
	r := gin.New()
	r.Any("/echo", func(c *gin.Context) {
		var sb strings.Builder
		for name, values := range c.Request.Header {
			for _, v := range values {
				sb.WriteString(name + ": " + v + "\n")
			}
		}
		c.Data(200, "text/plain", []byte(sb.String()))
	})
	r.NoRoute(func(c *gin.Context) {
		if c.Request.Method == "POST" {
			body, _ := io.ReadAll(c.Request.Body)
			c.Data(200, "text/plain", body)
			return
		}
		c.String(200, "OK")
	})
	r.Run("0.0.0.0:" + port)
}
```
