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
		switch string(ctx.Path()) {
		case "/echo":
			ctx.SetContentType("text/plain")
			ctx.Request.Header.VisitAll(func(key, value []byte) {
				ctx.WriteString(string(key) + ": " + string(value) + "\n")
			})
		default:
			if string(ctx.Method()) == "POST" {
				ctx.SetBody(ctx.Request.Body())
				return
			}
			ctx.SetBodyString("OK")
		}
	}

	fasthttp.ListenAndServe("0.0.0.0:"+port, handler)
}
