using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TodoMVC.Web.Infrastructure.Models;

namespace TodoMVC.Web.Infrastructure.Repository
{
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        private DbContext _context
        {
            get;
            set;
        }

        public GenericRepository()
            : this(new TrainingTodoEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = new DbContext(context, true);
        }

        public bool Create(T instance)
        {
            bool result = false;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                _context.Set<T>().Add(instance);
                result = SaveChanges();
            }
            return result;
        }

        public bool Delete(T instance)
        {
            bool result = false;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                result = SaveChanges();
            }
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public bool SaveChanges()
        {
            bool result = false;
            var sqlExecutedResult = _context.SaveChanges();
            if (sqlExecutedResult > 0)
            {
                result = true;
            }
            return result;
        }

        public bool Update(T instance)
        {
            bool result = false;
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                _context.Entry(instance).State = EntityState.Modified;
                result = SaveChanges();
            }
            return result;
        }
    }
}