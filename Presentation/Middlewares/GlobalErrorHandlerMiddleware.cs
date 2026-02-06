namespace Presentation.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        public GlobalErrorHandlerMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
        }
    }
}
