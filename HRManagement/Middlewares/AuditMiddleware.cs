using HRManagement.Data.Contexts;
using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HRManagement.Middlewares
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, HRManagementDbContext dbContext)
        {
            // Log the incoming request
            var request = context.Request;
            string requestBody = await ReadRequestBody(request);

            var requestAudit = new RequestAudit
            {
                Id = 0,
                RequestPath = request.Path,
                RequestMethod = request.Method,
                RequestBody = requestBody,
                ResponseBody = string.Empty,
                Timestamp = DateTime.UtcNow
            };

            dbContext.RequestAudits.Add(requestAudit);
            await dbContext.SaveChangesAsync();

            var originalResponseBodyStream = context.Response.Body;

            // Create a new memory stream to capture the response body
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // Log the response
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                requestAudit.ResponseBody = responseBodyText;

                // Restore the response body stream position to the beginning
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // Save the response body in the database
                await dbContext.SaveChangesAsync();

                await responseBody.CopyToAsync(originalResponseBodyStream);
            }
        }

        // Private method to read the request body
        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0; // Reset the position for the next middleware
                return body;
            }
        }
    }
}
