using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunkySwagger
{
    public class getReverse
    {
        private readonly ILogger<getReverse> _logger;

        public getReverse(ILogger<getReverse> log)
        {
            _logger = log;
        }

        [Function("getReverse")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string? input = req.Query["input"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                var data = JsonDocument.Parse(requestBody);
                if (data.RootElement.TryGetProperty("input", out var inputProp))
                {
                    input ??= inputProp.GetString();
                }
            }

            if (string.IsNullOrEmpty(input))
            {
                return new BadRequestObjectResult("Please pass an input string on the query string or in the request body.");
            }

            var reversed = new string(input.Reverse().ToArray());
            return new OkObjectResult(reversed);
        }
    }
}
