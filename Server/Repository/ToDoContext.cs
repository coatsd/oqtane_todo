using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Daniel.ToDo.Repository
{
    public class ToDoContext : DBContextBase, IService, IMultiDatabase
    {
        public virtual DbSet<Models.ToDo> ToDo { get; set; }

        public ToDoContext(ITenantManager tenantManager, IHttpContextAccessor accessor) : base(tenantManager, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
