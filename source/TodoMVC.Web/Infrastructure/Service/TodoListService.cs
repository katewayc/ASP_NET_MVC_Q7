using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoMVC.Web.Infrastructure.Models;
using TodoMVC.Web.Infrastructure.Repository;

namespace TodoMVC.Web.Infrastructure.Service
{
    public class TodoListService : ITodoListService<TodoList>
    {
        private ITodoListRepository todoListRepository;

        public TodoListService()
        {
            todoListRepository = new TodoListRepository();
        }

        public bool CheckTask(int todoId, bool doneOrNot)
        {
            bool result = false;
            result = todoListRepository.UpdateCompleted(todoId, doneOrNot);
            return result;
        }

        public bool CreateTask(string todoWhat)
        {
            bool result = false;
            if (todoWhat.Trim() != "")
            {
                TodoList task = new TodoList();
                task.TodoWhat = todoWhat;
                result = todoListRepository.Create(task);
            }

            return result;
        }

        public bool DeleteTask(int todoId)
        {
            bool result = false;
            result = todoListRepository.Delete(todoId);
            return result;
        }

        public IEnumerable<TodoList> GetTodoList(bool? completed, bool deleted)
        {
            IEnumerable<TodoList> todoList = null;

            if (completed != null)
            {
                todoList = this.todoListRepository.GetAllbyDeletedCompleted(deleted, (bool)completed).OrderByDescending(b => b.TodoId);
            }
            else
            {
                todoList = this.todoListRepository.GetAllbyDeleted(deleted).OrderByDescending(b => b.TodoId);
            }

            return todoList;
        }
    }
}