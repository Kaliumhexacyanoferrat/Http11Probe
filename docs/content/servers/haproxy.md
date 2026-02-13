---
title: "HAProxy"
toc: false
breadcrumbs: false
---

**Language:** C · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/HAProxyServer)

## Dockerfile

```dockerfile
FROM haproxy:3.0-alpine
COPY src/Servers/HAProxyServer/haproxy.cfg /usr/local/etc/haproxy/haproxy.cfg
COPY src/Servers/HAProxyServer/echo.lua /usr/local/etc/haproxy/echo.lua
```

## Source — `haproxy.cfg`

```text
global
    log stdout format raw local0
    lua-load /usr/local/etc/haproxy/echo.lua

defaults
    mode http
    timeout client 10s
    timeout connect 5s
    timeout server 10s

frontend http_in
    bind *:8080
    use_backend echo_backend if { path /echo }
    http-request return status 200 content-type "text/plain" string "OK"

backend echo_backend
    http-request use-service lua.echo
```

## Source — `echo.lua`

```lua
core.register_service("echo", "http", function(applet)
    local body = ""
    local hdrs = applet.headers
    for name, values in pairs(hdrs) do
        for _, v in ipairs(values) do
            body = body .. name .. ": " .. v .. "\n"
        end
    end
    applet:set_status(200)
    applet:add_header("Content-Type", "text/plain")
    applet:add_header("Content-Length", tostring(#body))
    applet:start_response()
    applet:send(body)
end)
```
