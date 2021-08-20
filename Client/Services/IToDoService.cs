using System.Collections.Generic;
using System.Threading.Tasks;
using Daniel.ToDo.Models;

namespace Daniel.ToDo.Services
{
    public interface IToDoService 
    {
        Task<List<Models.ToDo>> GetToDosAsync(int ModuleId);

        Task<Models.ToDo> GetToDoAsync(int ToDoId, int ModuleId);

        Task<Models.ToDo> AddToDoAsync(Models.ToDo ToDo);

        Task<Models.ToDo> UpdateToDoAsync(Models.ToDo ToDo);

        Task DeleteToDoAsync(int ToDoId, int ModuleId);
    }
}
