using System;
using TaskMini.Common;
using TaskMini.Exceptions;

namespace TaskMini.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                context.Response.ContentType = "application/json";

                _logger.LogError(ex, "Unhandled exception occurred");

                if (ex is AppException appException)
                {
                    context.Response.StatusCode=appException.StatusCode; 
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                await context.Response.WriteAsJsonAsync(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Data = null,
                        Message = ex.Message
                    });
            }

        }
    }
    
}
