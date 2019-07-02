using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public void Delete(int todoId)
        {
            TodoList todoList = db.TodoList.Find(todoId);
            todoList.Deleted = true;
            this.SaveChanges();
        }

        public void UpdateCompleted(int todoId, bool completed)
        {
            using (var context = new TrainingEntities())
            {
                string sql = "UPDATE TodoList SET Completed = @Completed Where TodoId = @TodoId";

                var p_todoId = (new SqlParameter("@TodoId", todoId));
                var p_completed = (new SqlParameter("@Completed", completed));

                var result = context.Database.ExecuteSqlCommand(sql, p_todoId, p_completed);
            }
        }

        public IEnumerable<TodoList> GetAllbyDeleted(bool deleted)
        {
            var where = PredicateBuilder.New<TodoList>();
            where = where.And(b => b.Deleted == deleted);
            return this.GetAll().Where(where.Compile());
        }

        public IEnumerable<TodoList> GetAllbyDeletedCompleted(bool deleted, bool completed)
        {
            var where = PredicateBuilder.New<TodoList>();
            where = where.And(b => b.Deleted == deleted);
            where = where.And(b => b.Completed == completed);
            return this.GetAll().Where(where.Compile());
        }

        public void UpdateDeleted(int todoId, bool deleted)
        {
            using (var context = new TrainingEntities())
            {
                string sql = "UPDATE TodoList SET Deleted = @Deleted Where TodoId = @TodoId";

                var p_todoId = (new SqlParameter("@TodoId", todoId));
                var p_deleted = (new SqlParameter("@Deleted", deleted));

                var result = context.Database.ExecuteSqlCommand(sql, p_todoId, p_deleted);
            }
        }
    }
}