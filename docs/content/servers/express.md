---
title: "Express"
toc: false
breadcrumbs: false
---

**Language:** JavaScript · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/ExpressServer)

## Dockerfile

```dockerfile
FROM node:22-slim
WORKDIR /app
COPY src/Servers/ExpressServer/package.json .
RUN npm install --omit=dev
COPY src/Servers/ExpressServer/server.js .
ENTRYPOINT ["node", "server.js", "8080"]
```

## Source — `server.js`

```javascript
const express = require("express");

const app = express();
const port = parseInt(process.argv[2] || "9003", 10);

app.get("/", (_req, res) => {
  res.send("OK");
});

app.post("/", (req, res) => {
  const chunks = [];
  req.on("data", (chunk) => chunks.push(chunk));
  req.on("end", () => res.send(Buffer.concat(chunks)));
});

app.listen(port, "127.0.0.1", () => {
  console.log(`Express listening on 127.0.0.1:${port}`);
});
```
