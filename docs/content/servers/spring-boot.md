---
title: "Spring Boot"
toc: false
breadcrumbs: false
---

**Language:** Java · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/SpringBootServer)

## Dockerfile

```dockerfile
FROM maven:3.9-eclipse-temurin-21 AS build
WORKDIR /src
COPY src/Servers/SpringBootServer/pom.xml .
RUN mvn dependency:go-offline -q
COPY src/Servers/SpringBootServer/src/ src/
RUN mvn package -q -DskipTests

FROM eclipse-temurin:21-jre
WORKDIR /app
COPY --from=build /src/target/*.jar app.jar
ENTRYPOINT ["java", "-jar", "app.jar", "--server.port=8080", "--server.address=127.0.0.1"]
```

## Source — `src/main/java/server/Application.java`

```java
package server;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import jakarta.servlet.http.HttpServletRequest;
import java.io.IOException;
import java.util.Enumeration;

@SpringBootApplication
@RestController
public class Application {

    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }

    @RequestMapping(value = "/", method = RequestMethod.GET)
    public String indexGet() {
        return "OK";
    }

    @RequestMapping(value = "/", method = RequestMethod.POST)
    public byte[] indexPost(HttpServletRequest request) throws IOException {
        return request.getInputStream().readAllBytes();
    }

    @RequestMapping("/echo")
    public ResponseEntity<String> echo(HttpServletRequest request) {
        StringBuilder sb = new StringBuilder();
        Enumeration<String> names = request.getHeaderNames();
        while (names.hasMoreElements()) {
            String name = names.nextElement();
            Enumeration<String> values = request.getHeaders(name);
            while (values.hasMoreElements()) {
                sb.append(name).append(": ").append(values.nextElement()).append("\n");
            }
        }
        return ResponseEntity.ok().contentType(MediaType.TEXT_PLAIN).body(sb.toString());
    }
}
```
