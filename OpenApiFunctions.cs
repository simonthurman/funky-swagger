using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace FunkySwagger
{
    public class OpenApiFunctions
    {
        [Function("OpenApiSpec")]
        public IActionResult GetSpec(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/openapi.json")] HttpRequest req)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("funky-swagger.openapi.json");
            if (stream is null)
                return new NotFoundResult();

            using var reader = new StreamReader(stream);
            return new ContentResult
            {
                Content = reader.ReadToEnd(),
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        [Function("SwaggerUI")]
        public IActionResult GetSwaggerUI(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequest req)
        {
            var html = """
                <!DOCTYPE html>
                <html>
                <head>
                    <title>funky-swagger - Swagger UI</title>
                    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swagger-ui-dist@5/swagger-ui.css" />
                </head>
                <body>
                    <div id="swagger-ui"></div>
                    <script src="https://cdn.jsdelivr.net/npm/swagger-ui-dist@5/swagger-ui-bundle.js"></script>
                    <script>
                        SwaggerUIBundle({
                            url: '/api/swagger/openapi.json',
                            dom_id: '#swagger-ui'
                        });
                    </script>
                </body>
                </html>
                """;

            return new ContentResult
            {
                Content = html,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
