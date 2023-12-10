using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelsBox.Generic.Domain.Repositories
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T model);
        Task<T> FindByIdAsync(int id);
        Task<T> FindByCodeAsync(int id);
        void Update(T model);
        void Delete(T model);
    }
}
