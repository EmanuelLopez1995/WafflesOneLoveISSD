using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Common.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Obtener(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
