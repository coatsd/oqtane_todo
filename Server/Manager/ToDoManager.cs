using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using Daniel.ToDo.Repository;

namespace Daniel.ToDo.Manager
{
    public class ToDoManager : MigratableModuleBase, IInstallable, IPortable
    {
        private IToDoRepository _ToDoRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IHttpContextAccessor _accessor;

        public ToDoManager(IToDoRepository ToDoRepository, ITenantManager tenantManager, IHttpContextAccessor accessor)
        {
            _ToDoRepository = ToDoRepository;
            _tenantManager = tenantManager;
            _accessor = accessor;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new ToDoContext(_tenantManager, _accessor), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new ToDoContext(_tenantManager, _accessor), tenant, MigrationType.Down);
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.ToDo> ToDos = _ToDoRepository.GetToDos(module.ModuleId).ToList();
            if (ToDos != null)
            {
                content = JsonSerializer.Serialize(ToDos);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.ToDo> ToDos = null;
            if (!string.IsNullOrEmpty(content))
            {
                ToDos = JsonSerializer.Deserialize<List<Models.ToDo>>(content);
            }
            if (ToDos != null)
            {
                foreach(var ToDo in ToDos)
                {
                    _ToDoRepository.AddToDo(new Models.ToDo { ModuleId = module.ModuleId, Name = ToDo.Name });
                }
            }
        }
    }
}