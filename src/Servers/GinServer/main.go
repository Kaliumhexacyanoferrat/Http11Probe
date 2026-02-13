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
