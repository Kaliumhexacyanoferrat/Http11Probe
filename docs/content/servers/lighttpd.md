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
RUN chmod +x /var/www/index.cgi
EXPOSE 8080
CMD ["lighttpd", "-D", "-f", "/etc/lighttpd/lighttpd.conf"]
```

## Source — `lighttpd.conf`

```text
server.document-root = "/var/www"
server.port = 8080
index-file.names = ("index.cgi")
server.modules += ("mod_cgi")
cgi.assign = (".cgi" => "")
server.error-handler = "/index.cgi"
```

## Source — `index.cgi`

```bash
#!/bin/sh
printf 'Content-Type: text/plain\r\n\r\n'
if [ "$REQUEST_METHOD" = "POST" ]; then
    cat
else
    printf 'OK'
fi
```
