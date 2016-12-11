using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace TodoSqlRepository
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            bool exists = _context.TodoItems.Any(i => i.Id == todoItem.Id);
            if (exists)
            {
                throw new DuplicateTodoItemException("duplicate id: " + todoItem.Id);
            }
            else
            {
                _context.TodoItems.Add(todoItem);
                _context.SaveChanges();
            }
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.Where(i => i.Id == todoId).FirstOrDefault();
            if (item != null)
            {
                checkPermissions(item.UserId, userId);
            }
            return item;
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItems.Where(i => i.UserId == userId && i.IsCompleted == false)
                           .ToList();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(i => i.UserId == userId)
                           .OrderByDescending(i => i.DateCreated)
                           .ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(i => i.UserId == userId && i.IsCompleted == true).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.Where(i => i.UserId == userId).Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.Where(i => i.Id == todoId).FirstOrDefault();
            if (item != null && checkPermissions(item.UserId, userId))
            {
                item.DateCompleted = DateTime.Now;
                item.IsCompleted = true;
                return _context.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.Where(i => i.Id == todoId).FirstOrDefault();
            if (item != null && checkPermissions(item.UserId, userId))
            {
                _context.TodoItems.Remove(item);
                return true;
            }
            return false;
        }


        public void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem item = _context.TodoItems.Where(i => i.Id == todoItem.Id).FirstOrDefault();
            if (item != null && checkPermissions(item.UserId, userId))
            {
                item = todoItem;
            }
            else
            {
                _context.TodoItems.Add(todoItem);
            }
            _context.SaveChanges();
        }

        private bool checkPermissions(Guid itemUserId, Guid userId)
        {
            if (itemUserId != userId)
            {
                throw new TodoAccessDeniedException("User with id <" + userId
                    + "> does not have permission to this item.");
            }
            else
            {
                return true;
            }
        }
    }
}
