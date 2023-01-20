using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieOnDemand.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _db;

        public EntityBaseRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(T t)
        {
            await _db.Set<T>().AddAsync(t);
            await _db.SaveChangesAsync();
        }
        

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _db.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _db.Set<T>().FirstOrDefaultAsync(m => m.Id == id);
            return result;
        }

        public async Task UpdateAsync(T t)
        {
            EntityEntry entityEntry = _db.Entry<T>(t);
            entityEntry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] expression)
        {
            IQueryable<T> query = _db.Set<T>();
            query = expression.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }
    }
}
