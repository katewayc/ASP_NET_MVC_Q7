using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using TodoMVC.Web.Models;
//using TodoMVC.Web.Services;
using TodoMVC.Web.Infrastructure.Models;
using TodoMVC.Web.Infrastructure.Service;

namespace TodoMVC.Web.Controllers
{
    public class TodoController : Controller
    {
        private ITodoListService<TodoList> todoListService;

        public TodoController()
        {
            this.todoListService = new TodoListService();
        }

        public ActionResult List(bool? completed = null, bool deleted = false)
        {
            IEnumerable<TodoList> todoList = todoListService.GetTodoList(completed, deleted);

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

        public ActionResult Check(int todoId, bool doneOrNot)
        {
            todoListService.CheckTask(todoId, doneOrNot);

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Delete(int todoId)
        {
            todoListService.DeleteTask(todoId);

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
