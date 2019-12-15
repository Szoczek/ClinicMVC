using Clinic.Database.Models;
using System;
using System.Collections.Generic;

namespace Clinic.Repositories.Abstract
{
    public interface IRepository<T> where T : BaseModel
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Update(T model);
        void Create(T model);
        void Delete(T model);
        void Delete(Guid id);
    }
}
