using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task AddAsync(T entities);

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes);


        public Task<T> GetByIdAsync(Expression<Func<T, bool>> expression, params string[] includes);


        public Task Update(T entities);

        public Task Remove(T entities);


        public Task<int> SaveAsync();

        public int Save();

        public Task<bool> isExist(Expression<Func<T, bool>> expression);
    }
}
