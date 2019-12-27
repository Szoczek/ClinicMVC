using Clinic.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Database.Abstract
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task Update(T entity);
        Task Create(T entity);
        Task Delete(T entity);
        Task DeleteById(Guid id);
    }
}
