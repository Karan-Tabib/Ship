using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShipAPI.Middleware
{

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering(); // Enable buffering to read the request body

            // Read the request body
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset the body stream position

                // Log the request method and body
                _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");
                if (body != null && !string.IsNullOrEmpty(body)) {
                    string bodyContent = body.Replace("\r\n", " ").Replace("\"", "").Trim();
                    _logger.LogInformation($"Request body: {JsonConvert.ToString(bodyContent)}");
                }
            }

            await _next(context); // Call the next middleware in the pipeline
            }
    }

}
