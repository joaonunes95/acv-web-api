using Database.Context;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity<TEntity>, new()
    {
        public readonly AcvContext Context;
        public readonly DbSet<TEntity> CurrentSet;

        private bool _disposed = false;

        protected Repository(AcvContext context)
        {
            Context = context;
            CurrentSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            Context.Add(entity);
        }

        public void AddMany(ICollection<TEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                Add(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await CurrentSet.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return await CurrentSet.Select(selector).ToListAsync();
        }

        public bool Any()
        {
            return CurrentSet.Any();
        }

        public async Task<bool> AnyAsync()
        {
            return await CurrentSet.AnyAsync();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return CurrentSet.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await CurrentSet.AnyAsync(predicate);
        }
        public void Update(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Context.Update(entity);
        }

        public TEntity GetById(Guid id)
        {
            return CurrentSet.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await CurrentSet.FindAsync(id);
        }

        public TResult GetById<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return CurrentSet.Where(x => x.Id == id).Select(selector).FirstOrDefault();
        }

        public async Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return await CurrentSet.Where(x => x.Id == id).Select(selector).FirstOrDefaultAsync();
        }

        public async Task<bool> CommitAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
