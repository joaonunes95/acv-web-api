using System;

namespace Domain
{
    public abstract class Entity<TEntity>
        where TEntity : Entity<TEntity>, new()
    { 
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
