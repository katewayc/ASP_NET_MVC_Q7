using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Repository
{
    public interface ITodoListRepository : IRepository<TodoList>
    {
        void UpdateCompleted(int TodoId, bool Completed);
        void UpdateDeleted(int TodoId, bool Deleted);
        void Delete(int TodoId);
        IEnumerable<TodoList> GetAllbyDeleted(bool deleted);
        IEnumerable<TodoList> GetAllbyDeletedCompleted(bool deleted, bool completed);
    }
}
