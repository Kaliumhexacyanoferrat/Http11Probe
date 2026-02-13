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