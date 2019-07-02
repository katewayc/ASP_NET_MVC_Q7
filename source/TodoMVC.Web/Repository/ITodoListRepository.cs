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
        void UpdateCompleted(int todoId, bool completed);
        void UpdateDeleted(int todoId, bool deleted);
        void Delete(int todoId);
        IEnumerable<TodoList> GetAllbyDeleted(bool deleted);
        IEnumerable<TodoList> GetAllbyDeletedCompleted(bool deleted, bool completed);
    }
}
