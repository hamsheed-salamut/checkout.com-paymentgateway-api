using Microsoft.EntityFrameworkCore;
using Payment.Infrastructure.Context;
using Payment.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Payment.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PaymentContext _context;
        private DbSet<T> _entity;

        public Repository()
        {

        }

        public Repository(PaymentContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.ToList();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _entity.SingleOrDefault(predicate);
        }
        public T GetById(object id)
        {
            return _entity.Find(id);
        }

        public void Insert(T entity)
        {
            _entity.Add(entity);
        }

        public void Update(T entity)
        {
            _entity.Add(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _entity.Find(id);
            _entity.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
