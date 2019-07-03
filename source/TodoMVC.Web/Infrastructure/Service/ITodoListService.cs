using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoMVC.Web.Infrastructure.Service
{
    interface ITodoListService<T>
        where T:class
    {
        IEnumerable<T> GetTodoList(bool? completed, bool deleted);

        bool CreateTask(string todoWhat);

        bool CheckTask(int todoId, bool doneOrNot);

        bool DeleteTask(int todoId);
    }
}