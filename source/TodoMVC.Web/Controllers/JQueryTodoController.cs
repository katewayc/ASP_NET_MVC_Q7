using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Services;

namespace TodoMVC.Web.Controllers
{
    public class JQueryTodoController : Controller
    {
        private TodoListService todoListServices;

        public JQueryTodoController()
        {
            todoListServices = new TodoListService();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList(bool? completed = null, bool deleted = false)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            IEnumerable<TodoList> todoList = todoListServices.GetTodoList(completed, deleted);

            var json = JsonConvert.SerializeObject(todoList, Formatting.None, setting);

            return Json(json);
        }

        [HttpPost]
        public JsonResult CreateTask(string taskTodo)
        {
            if (!string.IsNullOrEmpty(taskTodo))
            {
                todoListServices.CreateTask(taskTodo);
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult CheckTask(int todoId, bool checkToBe)
        {
            todoListServices.UpdateTaskCheckStatus(todoId, checkToBe);
            return Json("");
        }

        [HttpPost]
        public ActionResult DeleteTask(int todoId)
        {
            todoListServices.UpdateTaskAsDeleted(todoId);
            return Json("");
        }
    }
}