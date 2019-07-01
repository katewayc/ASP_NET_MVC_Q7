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

        public ActionResult Check(int TodoId, bool Completed)
        {
            todoListService.UpdateTaskCheckStatus(TodoId, Completed);

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int TodoId)
        {
            todoListService.UpdateTaskAsDeleted(TodoId);

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
