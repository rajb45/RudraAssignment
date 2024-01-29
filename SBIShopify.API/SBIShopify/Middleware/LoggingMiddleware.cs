using System.Text;

namespace SBIShopify.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            // Log the request
            await LogRequest(context);

            // Capture the response
            using var responseBody = new MemoryStream();
            
               context.Response.Body = responseBody;

                // Call the next middleware in the pipeline
                await _next(context);

                // Log the response
                await LogResponse(context, responseBody);
                // Copy the captured response back to the original stream
               await responseBody.CopyToAsync(originalBodyStream);
            
        }

        private async Task LogRequest(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();
            var requestLog = new StringBuilder();
            requestLog.AppendLine($"Request: {request.Method} {request.Path}");
            
            requestLog.AppendLine("Body:");
            requestLog.AppendLine(await ReadRequestBody(context));
            _logger.LogInformation(requestLog.ToString());
        }

        private async Task LogResponse(HttpContext context, MemoryStream responseBody)
        {
            var response = context.Response;
            var responseLog = new StringBuilder();
            responseLog.AppendLine($"Response: {response.StatusCode}");
            responseLog.AppendLine("Body:");
            responseLog.AppendLine(await ReadResponseBody(responseBody));
            _logger.LogInformation(responseLog.ToString());
        }

        private async Task<string> ReadRequestBody(HttpContext context)
        {
            var requestBody = string.Empty;
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Request.Body);
            requestBody = await reader.ReadToEndAsync();
            return requestBody;
        }

        private async Task<string> ReadResponseBody(MemoryStream responseBody)
        {
            string responseBodyContent = string.Empty;
            responseBody.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(responseBody);
            responseBodyContent = await reader.ReadToEndAsync();
            return responseBodyContent;
        }
    }

    //Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
