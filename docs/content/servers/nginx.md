---
title: "Nginx"
toc: false
breadcrumbs: false
---

**Language:** C · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/NginxServer)

## Dockerfile

```dockerfile
FROM nginx:1.27
COPY src/Servers/NginxServer/nginx.conf /etc/nginx/nginx.conf
COPY src/Servers/NginxServer/echo.js /etc/nginx/echo.js
```

## Source — `nginx.conf`

```nginx
load_module modules/ngx_http_js_module.so;

worker_processes 1;
pid /tmp/nginx.pid;
error_log /tmp/nginx_error.log;

events {
    worker_connections 64;
}

http {
    access_log /tmp/nginx_access.log;
    client_body_temp_path /tmp/nginx_client_body;
    proxy_temp_path       /tmp/nginx_proxy;
    fastcgi_temp_path     /tmp/nginx_fastcgi;
    uwsgi_temp_path       /tmp/nginx_uwsgi;
    scgi_temp_path        /tmp/nginx_scgi;

    js_import echo from /etc/nginx/echo.js;

    server {
        listen 8080;
        server_name localhost;

        location /echo {
            js_content echo.echo;
        }

        location / {
            return 200 "OK";
        }
    }
}
```

## Source — `echo.js`

```javascript
function echo(r) {
    var body = '';
    var headers = r.headersIn;
    for (var name in headers) {
        body += name + ': ' + headers[name] + '\n';
    }
    r.return(200, body);
}

export default { echo };
```
