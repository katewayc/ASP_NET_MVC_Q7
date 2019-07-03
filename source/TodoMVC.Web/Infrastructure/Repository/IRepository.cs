using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Web.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable
         where T : class
    {
        bool Create(T instance);

        bool Update(T instance);

        bool Delete(T instance);

        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll();

        bool SaveChanges();
    }
}
