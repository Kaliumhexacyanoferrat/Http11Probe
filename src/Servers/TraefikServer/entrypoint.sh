#!/bin/sh
/usr/local/bin/echo-server &
exec traefik "$@"
