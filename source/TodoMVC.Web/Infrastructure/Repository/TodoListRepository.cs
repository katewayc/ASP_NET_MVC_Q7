using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TodoMVC.Web.Infrastructure.Models;

namespace TodoMVC.Web.Infrastructure.Repository
{
    public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
    {
        protected TrainingTodoEntities db
        {
            get;
            private set;
        }

        public TodoListRepository()
        {
            db = new TrainingTodoEntities();
        }

        public IEnumerable<TodoList> GetAllbyDeleted(bool deleted)
        {
            var where = PredicateBuilder.New<TodoList>();
            where = where.And(b => b.Deleted == deleted);
            return GetAll().Where(where.Compile());
        }

        public IEnumerable<TodoList> GetAllbyDeletedCompleted(bool deleted, bool completed)
        {
            var where = PredicateBuilder.New<TodoList>();
            where = where.And(b => b.Deleted == deleted);
            where = where.And(b => b.Completed == completed);
            return GetAll().Where(where.Compile());
        }

        public bool UpdateCompleted(int todoId, bool doneOrNot)
        {
            bool result = false;
            using (var context = new TrainingTodoEntities())
            {
                string sql = "UPDATE TodoList SET Completed = @Completed Where TodoId = @TodoId";

                var p_todoId = (new SqlParameter("@TodoId", todoId));
                var p_completed = (new SqlParameter("@Completed", doneOrNot));

                var sqlExecutedResult = context.Database.ExecuteSqlCommand(sql, p_todoId, p_completed);
                if (sqlExecutedResult > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool Delete(int todoId)
        {
            bool result = false;
            using (var context = new TrainingTodoEntities())
            {
                string sql = "UPDATE TodoList SET Deleted = 1 Where TodoId = @TodoId";

                var p_todoId = (new SqlParameter("@TodoId", todoId));

                var sqlExecutedResult = context.Database.ExecuteSqlCommand(sql, p_todoId);
                if (sqlExecutedResult > 0)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}