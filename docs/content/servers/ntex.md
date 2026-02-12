---
title: "Ntex"
toc: false
breadcrumbs: false
---

**Language:** Rust · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/NtexServer)

## Dockerfile

```dockerfile
FROM rust:1-slim AS build
WORKDIR /src

# Cache dependencies with dummy main
COPY src/Servers/NtexServer/Cargo.toml .
RUN mkdir src && echo "fn main() {}" > src/main.rs && cargo build --release && rm -rf src target/release/.fingerprint/ntex-server-*

COPY src/Servers/NtexServer/src/ src/
RUN cargo build --release

FROM debian:bookworm-slim
COPY --from=build /src/target/release/ntex-server /usr/local/bin/
ENTRYPOINT ["ntex-server", "8080"]
```

## Source — `src/main.rs`

```rust
use ntex::web;
use ntex::util::Bytes;

async fn handler(req: web::HttpRequest, body: Bytes) -> web::HttpResponse {
    if req.method() == ntex::http::Method::POST {
        web::HttpResponse::Ok()
            .content_type("text/plain")
            .body(body)
    } else {
        web::HttpResponse::Ok()
            .content_type("text/plain")
            .body("OK")
    }
}

#[ntex::main]
async fn main() -> std::io::Result<()> {
    let port: u16 = std::env::args()
        .nth(1)
        .and_then(|s| s.parse().ok())
        .unwrap_or(8080);

    web::server(|| {
        web::App::new().default_service(web::to(handler))
    })
    .bind(("0.0.0.0", port))?
    .run()
    .await?;

    Ok(())
}
```
