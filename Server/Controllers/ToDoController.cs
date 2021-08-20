using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Daniel.ToDo.Repository;
using Oqtane.Controllers;
using System.Net;

namespace Daniel.ToDo.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class ToDoController : ModuleControllerBase
    {
        private readonly IToDoRepository _ToDoRepository;

        public ToDoController(IToDoRepository ToDoRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _ToDoRepository = ToDoRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.ToDo> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && ModuleId == AuthEntityId(EntityNames.Module))
            {
                return _ToDoRepository.GetToDos(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized ToDo Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.ToDo Get(int id)
        {
            Models.ToDo ToDo = _ToDoRepository.GetToDo(id);
            if (ToDo != null && ToDo.ModuleId == AuthEntityId(EntityNames.Module))
            {
                return ToDo;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized ToDo Get Attempt {ToDoId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.ToDo Post([FromBody] Models.ToDo ToDo)
        {
            if (ModelState.IsValid && ToDo.ModuleId == AuthEntityId(EntityNames.Module))
            {
                ToDo = _ToDoRepository.AddToDo(ToDo);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "ToDo Added {ToDo}", ToDo);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized ToDo Post Attempt {ToDo}", ToDo);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                ToDo = null;
            }
            return ToDo;
        }

        // PUT api/<controller>/5
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.ToDo Put(int id, [FromBody] Models.ToDo ToDo)
        {
            if (ModelState.IsValid && ToDo.ModuleId == AuthEntityId(EntityNames.Module) && _ToDoRepository.GetToDo(ToDo.ToDoId, false) != null)
            {
                ToDo = _ToDoRepository.UpdateToDo(ToDo);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "ToDo Updated {ToDo}", ToDo);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized ToDo Put Attempt {ToDo}", ToDo);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                ToDo = null;
            }
            return ToDo;
        }

        // DELETE api/<controller>/5
        [ValidateAntiForgeryToken]
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.ToDo ToDo = _ToDoRepository.GetToDo(id);
            if (ToDo != null && ToDo.ModuleId == AuthEntityId(EntityNames.Module))
            {
                _ToDoRepository.DeleteToDo(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "ToDo Deleted {ToDoId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized ToDo Delete Attempt {ToDoId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
