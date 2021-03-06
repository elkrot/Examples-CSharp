﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UI.Services.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task SaveAsync();
        bool HasChanges();
        void Add(T model);
        void Remove(T model);
    }
}
