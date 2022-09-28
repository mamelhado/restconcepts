using Microsoft.EntityFrameworkCore;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Infra.Data.Context;
using System.Linq.Expressions;

namespace Rest.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected BaseContext _context;
        public BaseRepository(BaseContext context) 
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll() 
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken token = default) 
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default) 
        {
            return await _context.Set<TEntity>().Where(expression).FirstOrDefaultAsync(token);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default)
        {
            return await _context.Set<TEntity>().Where(expression).SingleOrDefaultAsync(token);
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default, bool autoSave = true)
        {
            await _context.Set<TEntity>().AddAsync(entity, token);
            if (autoSave)
                SaveChanges(token);
            return entity;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken token = default, bool autoSave = true)
        {
            _context.Set<TEntity>().Update(entity);
            if (autoSave)
                SaveChanges(token);
        }

        public async Task DeleteAsync(int id, CancellationToken token = default) 
        {
            _context.Set<TEntity>().Remove(await GetByIdAsync(id));
            SaveChanges(token);
        }

        public async void SaveChanges(CancellationToken token = default) 
        {
            try
            {
                await _context.SaveChangesAsync(token);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
