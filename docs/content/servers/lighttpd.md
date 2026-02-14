---
title: "Lighttpd"
toc: false
breadcrumbs: false
---

**Language:** C · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/LighttpdServer)

## Dockerfile

```dockerfile
FROM alpine:3.20
RUN apk add --no-cache lighttpd
COPY src/Servers/LighttpdServer/lighttpd.conf /etc/lighttpd/lighttpd.conf
COPY src/Servers/LighttpdServer/index.cgi /var/www/index.cgi
COPY src/Servers/LighttpdServer/echo.cgi /var/www/echo.cgi
RUN chmod +x /var/www/index.cgi /var/www/echo.cgi
EXPOSE 8080
CMD ["lighttpd", "-D", "-f", "/etc/lighttpd/lighttpd.conf"]
```

## Source — `lighttpd.conf`

```text
server.document-root = "/var/www"
server.port = 8080
index-file.names = ("index.cgi")
server.modules += ("mod_cgi", "mod_alias")
cgi.assign = (".cgi" => "")
server.error-handler = "/index.cgi"
alias.url = ("/echo" => "/var/www/echo.cgi")
```

## Source — `index.cgi`

```bash
#!/bin/sh
printf 'Content-Type: text/plain\r\n\r\n'
if [ "$REQUEST_METHOD" = "POST" ] && [ "${CONTENT_LENGTH:-0}" -gt 0 ] 2>/dev/null; then
    head -c "$CONTENT_LENGTH"
else
    printf 'OK'
fi
```

## Source — `echo.cgi`

```bash
#!/bin/sh
printf 'Content-Type: text/plain\r\n\r\n'
env | grep '^HTTP_' | while IFS='=' read -r key value; do
    name=$(echo "$key" | sed 's/^HTTP_//;s/_/-/g')
    printf '%s: %s\n' "$name" "$value"
done
if [ -n "$CONTENT_TYPE" ]; then
    printf 'Content-Type: %s\n' "$CONTENT_TYPE"
fi
if [ -n "$CONTENT_LENGTH" ]; then
    printf 'Content-Length: %s\n' "$CONTENT_LENGTH"
fi
```
