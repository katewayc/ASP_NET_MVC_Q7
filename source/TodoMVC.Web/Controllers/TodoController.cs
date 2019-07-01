using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Repository;
using TodoMVC.Web.Service;

namespace TodoMVC.Web.Controllers
{
    public class TodoController : Controller
    {
        private TrainingEntities db = new TrainingEntities();
        private ITodoListRepository todoListRepository;
        private TodoListService todoListService;

        public TodoController()
        {
            this.todoListRepository = new TodoListRepository();
            this.todoListService = new TodoListService();
        }

        public ActionResult List(bool? Completed = null, bool Deleted = false)
        {
            IEnumerable<TodoList> todoList = todoListService.GetTodoList(Completed, Deleted);

            return View(todoList);
        }

        [HttpPost]
        public ActionResult Create(FormCollection post)
        {
            if (!string.IsNullOrEmpty(post["InputTodoWhat"]))
            {
                todoListService.CreateTask(post["InputTodoWhat"] as string);
            }

            return RedirectToAction("List");
        }

        public ActionResult Complete(int TodoId)
        {
            TodoList todoList = db.TodoList.Find(TodoId);
            if (todoList.Completed)
            {
                todoList.Completed = false;
            }
            else
            {
                todoList.Completed = true;
            }

            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int TodoId)
        {
            TodoList todoList = db.TodoList.Find(TodoId);
            todoList.Deleted = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
