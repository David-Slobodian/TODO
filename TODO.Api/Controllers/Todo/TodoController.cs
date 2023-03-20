using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using TODO.BL.TodoService;
using TODO.Dal.Entities;

namespace TODO.Api.Controllers.Todo
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService= todoService;
        }
        [HttpGet("get-items")]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await _todoService.GetAllTodoAsync());
        }
        [HttpGet("get-item")]
        public async Task<ActionResult<TodoEntity>> GetTodoItem(Guid id)
        {
            return Ok(await _todoService.GetTodoById(id));
        }
        [HttpPost("add-item")]
        public async Task<ActionResult<Guid>> AddTask(TodoEntity todo)
        {
            return Ok(await _todoService.CreateTodo(todo));
        }
        [HttpPut("update-item")]
        public async Task<ActionResult<bool>> UpdateTask(TodoEntity todo)
        {
            return Ok(await _todoService.UpdateTodo(todo));
        }
        [HttpDelete("delete-item")]
        public async Task<ActionResult<bool>> DeleteTask(Guid id)
        {
            return Ok(await _todoService.DeleteTodo(id));
        }
    }
}
