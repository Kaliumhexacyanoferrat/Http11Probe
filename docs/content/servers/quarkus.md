---
title: "Quarkus"
toc: false
breadcrumbs: false
---

**Language:** Java · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/QuarkusServer)

## Dockerfile

```dockerfile
FROM maven:3.9-eclipse-temurin-21 AS build
WORKDIR /src
COPY src/Servers/QuarkusServer/pom.xml .
RUN mvn dependency:go-offline -q
COPY src/Servers/QuarkusServer/src/ src/
RUN mvn package -q -DskipTests

FROM eclipse-temurin:21-jre
WORKDIR /app
COPY --from=build /src/target/quarkus-app/ quarkus-app/
ENTRYPOINT ["java", "-Dquarkus.http.port=8080", "-jar", "quarkus-app/quarkus-run.jar"]
```

## Source — `src/main/java/server/Application.java`

```java
package server;

import java.io.InputStream;
import java.io.IOException;

import jakarta.ws.rs.GET;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;

@Path("/")
public class Application {

    @GET
    @Path("{path:.*}")
    @Produces(MediaType.TEXT_PLAIN)
    public String catchAll() {
        return "OK";
    }

    @POST
    @Path("{path:.*}")
    @Produces(MediaType.TEXT_PLAIN)
    public byte[] catchAllPost(InputStream body) throws IOException {
        return body.readAllBytes();
    }
}
```
