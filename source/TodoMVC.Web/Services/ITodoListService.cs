using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Web.Services
{
    interface ITodoListService<T>
        where T : class
    {
        IEnumerable<T> GetTodoList(bool? completed, bool deleted);

        bool CreateTask(string todoWhat);

        bool UpdateTaskCheckStatus(int todoId, bool completed);

        bool UpdateTaskAsDeleted(int todoId);
    }
}
