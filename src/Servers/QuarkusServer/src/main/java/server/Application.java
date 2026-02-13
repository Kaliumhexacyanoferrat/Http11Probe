package server;

import java.io.InputStream;
import java.io.IOException;
import java.util.List;
import java.util.Map;

import jakarta.ws.rs.GET;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.Context;
import jakarta.ws.rs.core.HttpHeaders;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

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

    @GET
    @Path("/echo")
    @Produces(MediaType.TEXT_PLAIN)
    public Response echoGet(@Context HttpHeaders headers) {
        return echoHeaders(headers);
    }

    @POST
    @Path("/echo")
    @Produces(MediaType.TEXT_PLAIN)
    public Response echoPost(@Context HttpHeaders headers) {
        return echoHeaders(headers);
    }

    private Response echoHeaders(HttpHeaders headers) {
        StringBuilder sb = new StringBuilder();
        for (Map.Entry<String, List<String>> entry : headers.getRequestHeaders().entrySet()) {
            for (String value : entry.getValue()) {
                sb.append(entry.getKey()).append(": ").append(value).append("\n");
            }
        }
        return Response.ok(sb.toString(), MediaType.TEXT_PLAIN).build();
    }
}
