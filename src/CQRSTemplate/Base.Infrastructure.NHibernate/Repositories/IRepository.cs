namespace VLQR.Base.Infrastructure.EntityFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        T FindById(params object[] id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        IQueryable<T> ExecuteSql(string sql, params object[] parameters);
    }
}
