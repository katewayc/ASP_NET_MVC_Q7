using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Controllers
{
    public class JQueryTodoController : Controller
    {
        private TrainingEntities db = new TrainingEntities();

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetList(bool? Completed)
        {
            var todoList = db.TodoList.Where(b => b.Deleted == false).ToList().OrderByDescending(b => b.TodoId);

            if (Completed!=null)
            {
                todoList = db.TodoList.Where(b => b.Deleted == false & b.Completed == Completed).ToList().OrderByDescending(b => b.TodoId);
            }

            string jsonStr = "";

            jsonStr = JsonConvert.SerializeObject(todoList);

            return Json(jsonStr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTask(string TaskTodo)
        {
            TodoList todoList = new TodoList();
            todoList.TodoWhat = TaskTodo;
            if (todoList.TodoWhat != "")
            {
                db.TodoList.Add(todoList);
                db.SaveChanges();
            }

            string jsonStr = "";

            jsonStr = JsonConvert.SerializeObject(todoList);

            return Json(jsonStr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckTask(int TodoId)
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

        public ActionResult DeleteTask(int TodoId)
        {
            TodoList todoList = db.TodoList.Find(TodoId);
            todoList.Deleted = true;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}