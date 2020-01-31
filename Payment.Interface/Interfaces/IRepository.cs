using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Payment.Interface.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void Save();
    }
}
