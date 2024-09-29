namespace FoodsharingWebAPI.Middleware
{
    /// <summary>
    /// Перехватывает исключения, связанные с отменой задач, и логирует их
    /// </summary>
    public class TaskCancellationHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TaskCancellationHandlingMiddleware> _logger;
        public TaskCancellationHandlingMiddleware(RequestDelegate next, ILogger<TaskCancellationHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger; 
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
            {
                _logger.LogInformation("Task cancelled");
            }
        }
    }

}
