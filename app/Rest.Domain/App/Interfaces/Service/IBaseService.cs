using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App.Interfaces.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id, CancellationToken token = default);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);
        
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default);
        
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default);

        Task DeleteAsync(int id, CancellationToken token = default);
    }
}
