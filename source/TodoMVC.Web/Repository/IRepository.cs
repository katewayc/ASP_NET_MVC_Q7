using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Web.Repository
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Create(TEntity instance);

        void Update(TEntity instance);

        void Delete(TEntity instance);

        TEntity Get(Expression<Func<TEntity,bool>> predicate);

        IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> predicate);

        void SaveChanges();
    }
}
