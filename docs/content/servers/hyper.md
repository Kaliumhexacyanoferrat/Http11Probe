---
title: "Hyper"
toc: false
breadcrumbs: false
---

**Language:** Rust · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/HyperServer)

## Dockerfile

```dockerfile
FROM rust:1-slim AS build
WORKDIR /src

# Cache dependencies with dummy main
COPY src/Servers/HyperServer/Cargo.toml .
RUN mkdir src && echo "fn main() {}" > src/main.rs && cargo build --release && rm -rf src target/release/.fingerprint/hyper-server-*

COPY src/Servers/HyperServer/src/ src/
RUN cargo build --release

FROM debian:bookworm-slim
COPY --from=build /src/target/release/hyper-server /usr/local/bin/
ENTRYPOINT ["hyper-server", "8080"]
```

## Source — `src/main.rs`

```rust
use std::convert::Infallible;
use std::net::SocketAddr;

use http_body_util::Full;
use hyper::body::Bytes;
use hyper::server::conn::http1;
use hyper::service::service_fn;
use hyper::{Request, Response};
use hyper_util::rt::TokioIo;
use tokio::net::TcpListener;

async fn handle(req: Request<hyper::body::Incoming>) -> Result<Response<Full<Bytes>>, Infallible> {
    if req.method() == hyper::Method::POST {
        let body = match http_body_util::BodyExt::collect(req.into_body()).await {
            Ok(collected) => collected.to_bytes(),
            Err(_) => Bytes::new(),
        };
        return Ok(Response::new(Full::new(body)));
    }
    Ok(Response::new(Full::new(Bytes::from("OK"))))
}

#[tokio::main]
async fn main() {
    let port: u16 = std::env::args()
        .nth(1)
        .and_then(|s| s.parse().ok())
        .unwrap_or(8080);

    let addr = SocketAddr::from(([0, 0, 0, 0], port));
    let listener = TcpListener::bind(addr).await.unwrap();

    loop {
        let (stream, _) = listener.accept().await.unwrap();
        let io = TokioIo::new(stream);

        tokio::task::spawn(async move {
            if let Err(err) = http1::Builder::new()
                .serve_connection(io, service_fn(handle))
                .await
            {
                eprintln!("Error serving connection: {err:?}");
            }
        });
    }
}
```
