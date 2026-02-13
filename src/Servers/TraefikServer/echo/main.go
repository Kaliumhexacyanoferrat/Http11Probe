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
