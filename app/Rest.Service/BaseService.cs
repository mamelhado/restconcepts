using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;
using System.Linq.Expressions;

namespace Rest.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        public BaseService(IBaseRepository<TEntity> baseRepository) 
        {
            _baseRepository = baseRepository;
        }

        public async virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
        {
            return await _baseRepository.AddAsync(entity);
        }

        public async virtual Task DeleteAsync(int id, CancellationToken token = default)
        {
            await _baseRepository.DeleteAsync(id, token);
        }

        public IQueryable<TEntity> GetAll() 
        {
            return _baseRepository.GetAll();
        }

        public async virtual Task<TEntity> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _baseRepository.GetByIdAsync(id,token);
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression) 
        {
            return _baseRepository.GetWhere(expression);
        }

        public async virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default) 
        {
            return await _baseRepository.FirstOrDefaultAsync(expression, token);
        }

        public async virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token = default)
        {
            return await _baseRepository.SingleOrDefaultAsync(expression, token);
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
        {
            await _baseRepository.UpdateAsync(entity);
            return entity;
        }
    }
}