using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Daniel.ToDo.Models;

namespace Daniel.ToDo.Repository
{
    public class ToDoRepository : IToDoRepository, IService
    {
        private readonly ToDoContext _db;

        public ToDoRepository(ToDoContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.ToDo> GetToDos(int ModuleId)
        {
            return _db.ToDo.Where(item => item.ModuleId == ModuleId);
        }

        public Models.ToDo GetToDo(int ToDoId)
        {
            return GetToDo(ToDoId, true);
        }

        public Models.ToDo GetToDo(int ToDoId, bool tracking)
        {
            if (tracking)
            {
                return _db.ToDo.Find(ToDoId);
            }
            else
            {
                return _db.ToDo.AsNoTracking().FirstOrDefault(item => item.ToDoId == ToDoId);
            }
        }

        public Models.ToDo AddToDo(Models.ToDo ToDo)
        {
            _db.ToDo.Add(ToDo);
            _db.SaveChanges();
            return ToDo;
        }

        public Models.ToDo UpdateToDo(Models.ToDo ToDo)
        {
            _db.Entry(ToDo).State = EntityState.Modified;
            _db.SaveChanges();
            return ToDo;
        }

        public void DeleteToDo(int ToDoId)
        {
            Models.ToDo ToDo = _db.ToDo.Find(ToDoId);
            _db.ToDo.Remove(ToDo);
            _db.SaveChanges();
        }
    }
}
