using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id, CancellationToken token = default);

        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default, bool autoSave = true);

        Task UpdateAsync(TEntity entity, CancellationToken token = default, bool autoSave = true);

        Task DeleteAsync(int id, CancellationToken token = default);

        void SaveChanges(CancellationToken token = default);
    }
}
