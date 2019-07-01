﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoMVC.Web.Models;
using TodoMVC.Web.Repository;

namespace TodoMVC.Web.Service
{
    public class TodoListService
    {
        private ITodoListRepository todoListRepository;

        public TodoListService()
        {
            this.todoListRepository = new TodoListRepository();
        }
        public IEnumerable<TodoList> GetTodoList(bool? Completed, bool Deleted)
        {
            /*
                        > get all / get active/ get completed
                        > Order By Descending   
             */

            IEnumerable<TodoList> todoList = null;

            if (Completed != null)
            {
                todoList = this.todoListRepository.GetAllbyDeletedCompleted(Deleted, (bool)Completed).OrderByDescending(b => b.TodoId);
            }
            else
            {
                todoList = this.todoListRepository.GetAllbyDeleted(Deleted).OrderByDescending(b => b.TodoId);
            }

            return todoList;
        }

        public bool CreateTask(string TodoWhat)
        {
            TodoList task = new TodoList();
            task.TodoWhat = TodoWhat;
            this.todoListRepository.Create(task);

            return false;
        }
    }
}