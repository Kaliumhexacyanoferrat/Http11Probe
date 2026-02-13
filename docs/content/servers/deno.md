---
title: "Deno"
toc: false
breadcrumbs: false
---

**Language:** TypeScript · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/DenoServer)

## Dockerfile

```dockerfile
FROM denoland/deno:latest
COPY src/Servers/DenoServer/server.ts /app/server.ts
WORKDIR /app
RUN deno cache server.ts
EXPOSE 8080
CMD ["deno", "run", "--allow-net", "server.ts"]
```

## Source — `server.ts`

```typescript
Deno.serve({ port: 8080, hostname: "0.0.0.0" }, async (req) => {
  const url = new URL(req.url);
  if (url.pathname === "/echo") {
    let body = "";
    for (const [name, value] of req.headers) {
      body += name + ": " + value + "\n";
    }
    return new Response(body, { headers: { "content-type": "text/plain" } });
  }
  if (req.method === "POST") {
    const body = await req.text();
    return new Response(body, { headers: { "content-type": "text/plain" } });
  }
  return new Response("OK", { headers: { "content-type": "text/plain" } });
});
```
