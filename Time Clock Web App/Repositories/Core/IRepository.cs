using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Time_Clock_Web.Repositories.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(string id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void AddOrUpdate(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
