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

## Source — `webapp/WEB-INF/web.xml`

```xml
<?xml version="1.0" encoding="UTF-8"?>
<web-app xmlns="https://jakarta.ee/xml/ns/jakartaee" version="6.0">
    <servlet>
        <servlet-name>echo</servlet-name>
        <jsp-file>/echo.jsp</jsp-file>
    </servlet>
    <servlet-mapping>
        <servlet-name>echo</servlet-name>
        <url-pattern>/echo</url-pattern>
    </servlet-mapping>

    <servlet>
        <servlet-name>ok</servlet-name>
        <jsp-file>/ok.jsp</jsp-file>
    </servlet>
    <servlet-mapping>
        <servlet-name>ok</servlet-name>
        <url-pattern>/*</url-pattern>
    </servlet-mapping>
</web-app>
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

## Source — `webapp/echo.jsp`

```jsp
<%@page contentType="text/plain" import="java.util.*"%><%
Enumeration<String> names = request.getHeaderNames();
while (names.hasMoreElements()) {
    String name = names.nextElement();
    Enumeration<String> values = request.getHeaders(name);
    while (values.hasMoreElements()) {
        out.print(name + ": " + values.nextElement() + "\n");
    }
}
%>
```
