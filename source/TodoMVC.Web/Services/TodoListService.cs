using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoMVC.Web.Models;
using TodoMVC.Web.Repository;

namespace TodoMVC.Web.Services
{
    public class TodoListService : ITodoListService<TodoList>
    {
        private ITodoListRepository todoListRepository;

        public TodoListService()
        {
            this.todoListRepository = new TodoListRepository();
        }
        public IEnumerable<TodoList> GetTodoList(bool? completed, bool deleted)
        {
            /*
                        > get all / get active/ get completed
                        > Order By Descending   
             */

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

        public bool CreateTask(string todoWhat)
        {
            if (todoWhat.Trim() != "")
            {
                TodoList task = new TodoList();
                task.TodoWhat = todoWhat;
                this.todoListRepository.Create(task);
            }

            return false;
        }

        public bool UpdateTaskCheckStatus(int todoId, bool completed)
        {
            this.todoListRepository.UpdateCompleted(todoId, completed);
            return false;
        }

        public bool UpdateTaskAsDeleted(int todoId)
        {
            this.todoListRepository.UpdateDeleted(todoId, true);
            return false;
        }
    }
}