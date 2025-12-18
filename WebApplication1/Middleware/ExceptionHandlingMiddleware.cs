using System.Text.Json;
using WebApplication1.Responses;

namespace WebApplication1.Middleware
{
    /// <summary>
    /// глобальный middleware, перехватывает ошибки  и возвращает стандартизированный JSON
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// инициализирует middleware
        /// </summary>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        /// <summary>
        /// перехватывает выполнение следующего middleware, если возникает исключение, формирует ответ с кодом ошибки
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                int status = ex switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                var payload = ApiResponse<object>.Fail(status,
                    status == 500 ? "Internal Server Error" : ex.Message,
                    context.TraceIdentifier);

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json; charset=utf-8";

                await context.Response.WriteAsync(JsonSerializer.Serialize(payload,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }
        }
    }
}
