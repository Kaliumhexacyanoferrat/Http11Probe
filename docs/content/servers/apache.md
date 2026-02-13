---
title: "Apache"
toc: false
breadcrumbs: false
---

**Language:** C · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/ApacheServer)

## Dockerfile

```dockerfile
FROM httpd:2.4

COPY src/Servers/ApacheServer/httpd-probe.conf /usr/local/apache2/conf/httpd.conf
RUN echo "OK" > /usr/local/apache2/htdocs/index.html
COPY src/Servers/ApacheServer/echo.cgi /usr/local/apache2/cgi-bin/echo.cgi
RUN chmod +x /usr/local/apache2/cgi-bin/echo.cgi
```

## Source — `httpd-probe.conf`

```apache
ServerRoot "/usr/local/apache2"
Listen 8080

LoadModule mpm_event_module modules/mod_mpm_event.so
LoadModule dir_module modules/mod_dir.so
LoadModule unixd_module modules/mod_unixd.so
LoadModule authz_core_module modules/mod_authz_core.so
LoadModule cgi_module modules/mod_cgi.so
LoadModule alias_module modules/mod_alias.so

ErrorLog /proc/self/fd/2
LogLevel warn

DocumentRoot "/usr/local/apache2/htdocs"

<Directory "/usr/local/apache2/htdocs">
    Require all granted
</Directory>

ScriptAlias /echo /usr/local/apache2/cgi-bin/echo.cgi

<Directory "/usr/local/apache2/cgi-bin">
    Require all granted
</Directory>
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
