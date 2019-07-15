using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
//using TodoMVC.Web.Models;
//using TodoMVC.Web.Services;
using TodoMVC.Web.Infrastructure.Models;
using TodoMVC.Web.Infrastructure.Service;

namespace TodoMVC.Web.Controllers
{
    public class JQueryTodoController : Controller
    {
        private ITodoListService<TodoList> todoListService;

        public JQueryTodoController()
        {
            todoListService = new TodoListService();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(bool? completed = null, bool deleted = false)
        {
            string result = "";
            try
            {
                IEnumerable<TodoList> todoList = todoListService.GetTodoList(completed, deleted);

                var settingsForCamelCasePropertyNames = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

                var json = JsonConvert.SerializeObject(todoList, Formatting.None, settingsForCamelCasePropertyNames);
                result = json;
            }
            catch (Exception x)
            {
                result = x.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult CreateTask(string taskTodo)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(taskTodo))
            {
                result = todoListService.CreateTask(taskTodo);
            }
            return Content(result.ToString());
        }

        [HttpPost]
        public ActionResult CheckTask(int todoId, bool doneOrNot)
        {
            bool result = false;
            result = todoListService.CheckTask(todoId, doneOrNot);
            return Content(result.ToString());
        }

        [HttpPost]
        public ActionResult DeleteTask(int todoId)
        {
            bool result = false;
            result = todoListService.DeleteTask(todoId);
            return Content(result.ToString());
        }
    }
}