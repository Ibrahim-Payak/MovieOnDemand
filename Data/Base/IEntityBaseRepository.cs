using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieOnDemand.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T t);

        Task UpdateAsync(T t);

        Task DeleteAsync(int id);

        Task<T> GetByIdAsync(int id);
    }
}
