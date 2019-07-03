﻿using Newtonsoft.Json;
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
            IEnumerable<TodoList> todoList = todoListService.GetTodoList(completed, deleted);

            var settingsForCamelCasePropertyNames = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            var json = JsonConvert.SerializeObject(todoList, Formatting.None, settingsForCamelCasePropertyNames);

            return Json(json);
        }

        [HttpPost]
        public ActionResult CreateTask(string taskTodo)
        {
            if (!string.IsNullOrEmpty(taskTodo))
            {
                todoListService.CreateTask(taskTodo);
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult CheckTask(int todoId, bool doneOrNot)
        {
            todoListService.CheckTask(todoId, doneOrNot);
            return Json("");
        }

        [HttpPost]
        public ActionResult DeleteTask(int todoId)
        {
            todoListService.DeleteTask(todoId);
            return Json("");
        }
    }
}