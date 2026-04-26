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
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] TaskRequestJson request)
        {
            var response = _taskService.Create(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TaskResponseJson>), StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(TaskResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var tasks = _taskService.GetById(id);
            return Ok(tasks);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(UpdateTaskResponseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromRoute] Guid id, [FromBody] TaskRequestJson request)
        {
            var taskUpdated = _taskService.Update(id, request);
            return Ok(taskUpdated);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _taskService.Delete(id);
            return NoContent();
        }
    }
}
