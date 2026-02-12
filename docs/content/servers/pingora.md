---
title: "Pingora"
toc: false
breadcrumbs: false
---

**Language:** Rust · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/PingoraServer)

## Dockerfile

```dockerfile
FROM rust:1-slim AS build
RUN apt-get update && apt-get install -y pkg-config libssl-dev cmake g++ && rm -rf /var/lib/apt/lists/*
WORKDIR /src

# Cache dependencies with dummy main
COPY src/Servers/PingoraServer/Cargo.toml .
RUN mkdir src && echo "fn main() {}" > src/main.rs && cargo build --release && rm -rf src target/release/.fingerprint/pingora-server-*

COPY src/Servers/PingoraServer/src/ src/
RUN cargo build --release

FROM debian:bookworm-slim
RUN apt-get update && apt-get install -y libssl3 && rm -rf /var/lib/apt/lists/*
COPY --from=build /src/target/release/pingora-server /usr/local/bin/
ENTRYPOINT ["pingora-server", "8080"]
```

## Source — `src/main.rs`

```rust
use async_trait::async_trait;
use bytes::Bytes;
use pingora::http::ResponseHeader;
use pingora::prelude::*;
use pingora::proxy::{http_proxy_service, ProxyHttp, Session};

struct OkProxy;

#[async_trait]
impl ProxyHttp for OkProxy {
    type CTX = ();

    fn new_ctx(&self) -> Self::CTX {}

    async fn request_filter(
        &self,
        session: &mut Session,
        _ctx: &mut Self::CTX,
    ) -> Result<bool> {
        let is_post = session.req_header().method == pingora::http::Method::POST;
        let body = if is_post {
            let mut buf = Vec::new();
            while let Some(chunk) = session.read_request_body().await? {
                buf.extend_from_slice(&chunk);
            }
            Bytes::from(buf)
        } else {
            Bytes::from_static(b"OK")
        };
        let mut header = ResponseHeader::build(200, None)?;
        header.insert_header("Content-Type", "text/plain")?;
        header.insert_header("Content-Length", &body.len().to_string())?;
        session
            .write_response_header(Box::new(header), false)
            .await?;
        session
            .write_response_body(Some(body), true)
            .await?;
        Ok(true)
    }

    async fn upstream_peer(
        &self,
        _session: &mut Session,
        _ctx: &mut Self::CTX,
    ) -> Result<Box<HttpPeer>> {
        // Never reached — request_filter always handles the request
        unreachable!()
    }
}

fn main() {
    let port: u16 = std::env::args()
        .nth(1)
        .and_then(|s| s.parse().ok())
        .unwrap_or(9011);

    let mut server = Server::new(None).unwrap();
    server.bootstrap();

    let mut proxy = http_proxy_service(&server.configuration, OkProxy);
    proxy.add_tcp(&format!("0.0.0.0:{port}"));
    server.add_service(proxy);

    server.run_forever();
}
```
