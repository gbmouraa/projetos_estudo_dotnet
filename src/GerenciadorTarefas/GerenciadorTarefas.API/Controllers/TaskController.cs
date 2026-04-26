using GerenciadorTarefas.Application.Services;
using GerenciadorTarefas.Communication.Requests;
using GerenciadorTarefas.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskResponseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson),StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateTaskRequestJson request)
        {
            var response = _taskService.Create(request);
            return Created(string.Empty, response);
        }
    }
}
