---
title: "Bun"
toc: false
breadcrumbs: false
---

**Language:** TypeScript · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/BunServer)

## Dockerfile

```dockerfile
FROM oven/bun:1-slim
WORKDIR /app
COPY src/Servers/BunServer/server.ts .
ENTRYPOINT ["bun", "run", "server.ts", "8080"]
```

## Source — `server.ts`

```typescript
const port = parseInt(Bun.argv[2] || "8080", 10);

Bun.serve({
  port,
  hostname: "0.0.0.0",
  async fetch(req) {
    const url = new URL(req.url);
    if (url.pathname === "/echo") {
      let body = "";
      for (const [name, value] of req.headers) {
        body += name + ": " + value + "\n";
      }
      return new Response(body, { headers: { "Content-Type": "text/plain" } });
    }
    if (req.method === "POST") {
      const body = await req.text();
      return new Response(body);
    }
    return new Response("OK");
  },
});

console.log(`Bun listening on 127.0.0.1:${port}`);
```
