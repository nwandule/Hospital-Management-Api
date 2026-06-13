/*=============================================================================
 * Author:       Vikash
 * Description:  Global exception interceptor middleware. Catches unhandled 
 * service exceptions and returns clean, structured JSON payloads.
 *=============================================================================*/
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Hospital_Management_system.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // 🟢 Determine the correct HTTP Status Code based on the exception type
            if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
            }
            else if (exception is ArgumentException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
            }

            // Create a structured error response payload
            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = exception.Message,
                detailed = exception.InnerException?.Message // Optional tracking
            };

            var jsonResult = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResult);
        }
    }
}