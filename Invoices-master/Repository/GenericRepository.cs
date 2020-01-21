using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Invoices.Model;

namespace Invoices.Repository
{
    public class GenericRepository<C, T> : IGenericRepository<T>
        where T : class
        where C : DbContext
    {
        private C _entities;
        public GenericRepository(C entities)
        {
            this._entities = entities;
        }

        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var set = _entities.Set<T>();
            IQueryable<T> query = set.Where(predicate); //.Were comes from Queryable object
            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T invoice)
        {
            _entities.Set<T>().Remove(invoice);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
