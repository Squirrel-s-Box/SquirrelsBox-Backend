﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelsBox.Generic.Domain.Services
{
    public interface IGenericReadService<T, R>
    {
        Task<IEnumerable<R>> ListAllByUserCodeAsync(string userCode);
        Task<IEnumerable<R>> ListAllByIdCodeAsync(int id);
    }
}
