using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVC.Web.Infrastructure.Models;

namespace TodoMVC.Web.Infrastructure.Repository
{
    interface ITodoListRepository : IRepository<TodoList>
    {
        bool Delete(int todoId);

        bool UpdateCompleted(int todoId, bool doneOrNot);

        IEnumerable<TodoList> GetAllbyDeleted(bool deleted);

        IEnumerable<TodoList> GetAllbyDeletedCompleted(bool deleted, bool completed);
    }
}
