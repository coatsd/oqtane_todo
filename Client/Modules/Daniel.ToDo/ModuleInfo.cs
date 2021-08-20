using Oqtane.Models;
using Oqtane.Modules;

namespace Daniel.ToDo
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "ToDo",
            Description = "ToDo",
            Version = "1.0.0",
            ServerManagerType = "Daniel.ToDo.Manager.ToDoManager, Daniel.ToDo.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "Daniel.ToDo.Shared.Oqtane"
        };
    }
}
