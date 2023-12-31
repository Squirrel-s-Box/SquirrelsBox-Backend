﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelsBox.Generic.Domain.Services
{
    public interface IGenericService<T, R>
    {
        Task<R> SaveAsync(T model);
        Task<R> FindByIdAsync(int id);
        Task<R> FindByCodeAsync(string value);
        Task<R> UpdateAsync(int id, T model);
        Task<R> DeleteAsync(int id);
    }
}
