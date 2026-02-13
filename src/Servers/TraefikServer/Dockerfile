FROM alpine:3.20 AS plugin
RUN apk add --no-cache git
RUN git clone https://github.com/jdel/staticresponse.git /plugin

FROM golang:1.23-alpine AS echo-build
WORKDIR /build
COPY src/Servers/TraefikServer/echo/main.go main.go
RUN go build -o /echo-server main.go

FROM traefik:v3.2
COPY --from=plugin /plugin /plugins-local/src/github.com/jdel/staticresponse/
COPY --from=echo-build /echo-server /usr/local/bin/echo-server
COPY src/Servers/TraefikServer/traefik.yml /etc/traefik/traefik.yml
COPY src/Servers/TraefikServer/dynamic.yml /etc/traefik/dynamic.yml
COPY src/Servers/TraefikServer/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]
