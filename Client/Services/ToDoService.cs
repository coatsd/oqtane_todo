using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Daniel.ToDo.Models;

namespace Daniel.ToDo.Services
{
    public class ToDoService : ServiceBase, IToDoService, IService
    {
        public ToDoService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("ToDo");

        public async Task<List<Models.ToDo>> GetToDosAsync(int ModuleId)
        {
            List<Models.ToDo> ToDos = await GetJsonAsync<List<Models.ToDo>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId));
            return ToDos.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.ToDo> GetToDoAsync(int ToDoId, int ModuleId)
        {
            return await GetJsonAsync<Models.ToDo>(CreateAuthorizationPolicyUrl($"{Apiurl}/{ToDoId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.ToDo> AddToDoAsync(Models.ToDo ToDo)
        {
            return await PostJsonAsync<Models.ToDo>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, ToDo.ModuleId), ToDo);
        }

        public async Task<Models.ToDo> UpdateToDoAsync(Models.ToDo ToDo)
        {
            return await PutJsonAsync<Models.ToDo>(CreateAuthorizationPolicyUrl($"{Apiurl}/{ToDo.ToDoId}", EntityNames.Module, ToDo.ModuleId), ToDo);
        }

        public async Task DeleteToDoAsync(int ToDoId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{ToDoId}", EntityNames.Module, ModuleId));
        }
    }
}
