using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>, new()
    {
        void Add(TEntity entity);
        void AddMany(ICollection<TEntity> entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
        Task<IEnumerable<TEntity>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
        void Update(TEntity entity);




        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        TResult GetById<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector);
        Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector);



        Task<bool> CommitAsync();

        bool Any();
        Task<bool> AnyAsync();
        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
