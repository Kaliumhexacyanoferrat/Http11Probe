---
title: "Actix"
toc: false
breadcrumbs: false
---

**Language:** Rust · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/ActixServer)

## Dockerfile

```dockerfile
FROM rust:1-slim AS build
WORKDIR /src

# Cache dependencies with dummy main
COPY src/Servers/ActixServer/Cargo.toml .
RUN mkdir src && echo "fn main() {}" > src/main.rs && cargo build --release && rm -rf src target/release/.fingerprint/actix-server-*

COPY src/Servers/ActixServer/src/ src/
RUN cargo build --release

FROM debian:bookworm-slim
COPY --from=build /src/target/release/actix-server /usr/local/bin/
ENTRYPOINT ["actix-server", "8080"]
```

## Source — `src/main.rs`

```rust
use actix_web::{web, App, HttpServer, HttpRequest, HttpResponse, Responder};

async fn echo(req: HttpRequest) -> impl Responder {
    let mut body = String::new();
    for (name, value) in req.headers() {
        body.push_str(&format!("{}: {}\n", name, value.to_str().unwrap_or("")));
    }
    HttpResponse::Ok().content_type("text/plain").body(body)
}

async fn handler(req: HttpRequest, body: web::Bytes) -> HttpResponse {
    if req.method() == actix_web::http::Method::POST {
        HttpResponse::Ok()
            .content_type("text/plain")
            .body(body)
    } else {
        HttpResponse::Ok()
            .content_type("text/plain")
            .body("OK")
    }
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    let port: u16 = std::env::args()
        .nth(1)
        .and_then(|s| s.parse().ok())
        .unwrap_or(8080);

    HttpServer::new(|| {
        App::new()
            .route("/echo", web::to(echo))
            .default_service(web::to(handler))
    })
    .bind(("0.0.0.0", port))?
    .run()
    .await
}
```
