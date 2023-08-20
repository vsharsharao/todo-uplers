using Microsoft.AspNetCore.Mvc;
using todo.server.Services.Interfaces;
using todo.server.Models;

namespace todo.server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoTask>> Get()
        {
            return Ok(_todoService.GetTasks());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoTask> Get(long id)
        {
            return Ok(_todoService.GetTaskById(id));
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> Post([FromBody] TodoTask todoTask)
        {
            var task = await _todoService.CreateTaskAsync(todoTask);
            return Created($"/todo/{task.Id}", task);
        }

        [HttpPut]
        public async Task<ActionResult<TodoTask>> Put([FromBody] TodoTask todoTask)
        {
            var updatedTask = await _todoService.UpdateTaskAsync(todoTask);
            return Ok(updatedTask);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoTask>> Patch(long id, bool isCompleted)
        {
            var updatedTask = await _todoService.UpdateAsCompletedAsync(id, isCompleted);
            return Ok(updatedTask);
        }

        [HttpPatch]
        public async Task<ActionResult<TodoTask>> Patch([FromBody]IEnumerable<long> taskIds)
        {
            var updatedTasks = await _todoService.UpdateAsCompletedAsync(taskIds);
            return Ok(updatedTasks);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await _todoService.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] IEnumerable<long> taskIds)
        {
            await _todoService.DeleteTaskAsync(taskIds);
            return Ok();
        }
    }
}