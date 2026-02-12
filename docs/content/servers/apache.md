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
```

## Source — `httpd-probe.conf`

```apacheconf
ServerRoot "/usr/local/apache2"
Listen 8080

LoadModule mpm_event_module modules/mod_mpm_event.so
LoadModule dir_module modules/mod_dir.so
LoadModule unixd_module modules/mod_unixd.so
LoadModule authz_core_module modules/mod_authz_core.so

ErrorLog /proc/self/fd/2
LogLevel warn

DocumentRoot "/usr/local/apache2/htdocs"

<Directory "/usr/local/apache2/htdocs">
    Require all granted
</Directory>
```
