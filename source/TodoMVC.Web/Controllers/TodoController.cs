using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Controllers
{
    public class TodoController : Controller
    {
        private TrainingEntities db = new TrainingEntities();

        public ActionResult List()
        {
            var todoList = db.TodoList.Where(b => b.Deleted == false).ToList();

            return View(todoList);
        }

        public ActionResult ActiveTodoList()
        {
            var todoList = db.TodoList.Where(b => b.Deleted == false & b.Completed == false).ToList();

            return View("List", todoList);
        }

        public ActionResult CompletedList()
        {
            var todoList = db.TodoList.Where(b => b.Deleted == false & b.Completed == true).ToList();

            return View("List", todoList);
        }

        [HttpPost]
        public ActionResult Create(FormCollection post)
        {
            TodoList todoList = new TodoList();
            todoList.TodoWhat = post["InputTodoWhat"].ToString();
            if (todoList.TodoWhat != "")
            {
                db.TodoList.Add(todoList);
                db.SaveChanges();
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
