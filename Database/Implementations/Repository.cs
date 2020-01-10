using Clinic.Database.Abstract;
using Clinic.Database.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Database.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _set;

        public Repository(MongoDbContext context)
        {
            _context = context;
            _set = _context.DbSet<T>();
        }
        public async Task Create(T entity)
        {
            if (entity.Id == null || entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();

            await _set.InsertOneAsync(entity);
        }
        public async Task Delete(T entity)
        {
            await _set.DeleteOneAsync(x => x == entity);

        }
        public async Task DeleteById(Guid id)
        {
            await _set.DeleteOneAsync(x => x.Id == id);
        }
        public async Task<T> GetById(Guid id)
        {
            using var cursor = await _set.FindAsync(Builders<T>.Filter.Where(x => x.Id == id));
            while (await cursor.MoveNextAsync())
                return await cursor.FirstOrDefaultAsync();

            return null;
        }
        public async Task<List<T>> GetAll()
        {
            return await _set.Find(x => true).ToListAsync();
        }
        public async Task Update(T entity)
        {
            await _set.ReplaceOneAsync(x => x.Id == entity.Id,
                entity);
        }
    }
}
