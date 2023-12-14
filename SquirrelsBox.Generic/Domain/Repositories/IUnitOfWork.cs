using Microsoft.EntityFrameworkCore;

namespace SquirrelsBox.Generic.Domain.Repositories
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task CompleteAsync();
    }
}
