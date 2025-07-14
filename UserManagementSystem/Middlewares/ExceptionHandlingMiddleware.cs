namespace UserManagementSystem.Middlewares
{
    
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("An error occurred.");
            }
        }
    }

}
