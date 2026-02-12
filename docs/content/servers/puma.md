---
title: "Puma"
toc: false
breadcrumbs: false
---

**Language:** Ruby · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/PumaServer)

## Dockerfile

```dockerfile
FROM ruby:3.3-slim
RUN apt-get update && apt-get install -y --no-install-recommends build-essential && \
    gem install puma --no-doc && \
    apt-get purge -y build-essential && apt-get autoremove -y && \
    rm -rf /var/lib/apt/lists/*
COPY src/Servers/PumaServer/config.ru /app/config.ru
WORKDIR /app
EXPOSE 8080
CMD ["puma", "-b", "tcp://0.0.0.0:8080"]
```

## Source — `config.ru`

```ruby
app = proc { |env|
  if env['REQUEST_METHOD'] == 'POST'
    body = env['rack.input'].read
    [200, { 'content-type' => 'text/plain' }, [body]]
  else
    [200, { 'content-type' => 'text/plain' }, ['OK']]
  end
}
run app
```
