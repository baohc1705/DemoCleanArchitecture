using DemoCleanArchitecture.API.Common;
using FluentValidation;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoCleanArchitecture.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            { 
                await HandleException(context, ex); 
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            string message = "Lỗi hệ thống";
            List<string> errors = new();

            switch (ex)
            {
                case ValidationException ve:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "Dữ liệu không hợp lệ.";
                    errors = ve.Errors.Select(e => e.ErrorMessage).ToList();
                    break;
            }

            var response = ApiResponse<object>.Fail(message, statusCode, errors);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
