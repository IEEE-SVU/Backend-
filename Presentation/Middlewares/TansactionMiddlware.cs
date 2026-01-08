using Infrastructure.DataBase;

namespace Presentation.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        public readonly Context _context;
        public TransactionMiddleware(Context context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await next(context);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
