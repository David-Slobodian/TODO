using TODO.Dal.Entities;

namespace TODO.BL.TodoService
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoEntity>> GetAllTodoAsync();
        Task<TodoEntity> GetTodoById(Guid id);

        Task<Guid> CreateTodo(TodoEntity entity);
        Task<bool> UpdateTodo(TodoEntity entity);
        Task<bool> DeleteTodo(Guid id);
    }
}