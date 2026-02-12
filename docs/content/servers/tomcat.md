---
title: "Tomcat"
toc: false
breadcrumbs: false
---

**Language:** Java · [View source on GitHub](https://github.com/MDA2AV/Http11Probe/tree/main/src/Servers/TomcatServer)

## Dockerfile

```dockerfile
FROM tomcat:11-jre21-temurin
RUN rm -rf /usr/local/tomcat/webapps/*
COPY src/Servers/TomcatServer/webapp/ /usr/local/tomcat/webapps/ROOT/
EXPOSE 8080
CMD ["catalina.sh", "run"]
```

## Source — `webapp/ok.jsp`

```jsp
<%@page contentType="text/plain" import="java.io.*"%><%
if ("POST".equals(request.getMethod())) {
    InputStream in = request.getInputStream();
    byte[] buf = in.readAllBytes();
    out.print(new String(buf, "UTF-8"));
} else {
    out.print("OK");
}
%>
```
