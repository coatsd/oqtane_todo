using System.Collections.Generic;
using Daniel.ToDo.Models;

namespace Daniel.ToDo.Repository
{
    public interface IToDoRepository
    {
        IEnumerable<Models.ToDo> GetToDos(int ModuleId);
        Models.ToDo GetToDo(int ToDoId);
        Models.ToDo GetToDo(int ToDoId, bool tracking);
        Models.ToDo AddToDo(Models.ToDo ToDo);
        Models.ToDo UpdateToDo(Models.ToDo ToDo);
        void DeleteToDo(int ToDoId);
    }
}
