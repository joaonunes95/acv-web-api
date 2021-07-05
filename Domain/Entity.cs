using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public abstract class Entity<TEntity>
        where TEntity : Entity<TEntity>, new()
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
