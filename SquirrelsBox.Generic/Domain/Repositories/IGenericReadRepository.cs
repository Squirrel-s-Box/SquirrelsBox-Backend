using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelsBox.Generic.Domain.Repositories
{
    public interface IGenericReadRepository<T>
    {
        Task<IEnumerable<T>> ListAllByUserCodeAsync(string userCode);
    }
}
