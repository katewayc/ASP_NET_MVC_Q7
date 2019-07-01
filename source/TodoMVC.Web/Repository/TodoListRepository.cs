using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Repository
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        protected TrainingEntities db
        {
            get;
            private set;
        }

        public TodoListRepository()
        {
            this.db = new TrainingEntities();
        }

        public void Delete(int TodoId)
        {
            TodoList todoList = db.TodoList.Find(TodoId);
            todoList.Deleted = true;
            this.SaveChanges();
        }

        public void UpdateCompleted(TodoList instance)
        {
            if (instance != null)
            {
                throw new ArgumentNullException("instance = null");
            }
            else
            {
                string sql = "";
                db.Database.ExecuteSqlCommand(sql, instance);
            }
        }

        public IEnumerable<TodoList> GetAllbyDeleted(bool deleted)
        {
            var where = PredicateBuilder.New<TodoList>();
            where = where.And(b => b.Deleted == deleted);
            return this.GetAll().Where(where.Compile());
        }

        public IEnumerable<TodoList> GetAllbyDeletedCompleted(bool deleted,bool completed)
        {
            var where = PredicateBuilder.New<TodoList>();
            where = where.And(b => b.Deleted == deleted);
            where = where.And(b => b.Completed == completed);
            return this.GetAll().Where(where.Compile());
        }
    }
}