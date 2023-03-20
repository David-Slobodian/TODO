using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Dal;
using TODO.Dal.Entities;
using TODO.Dal.Repos;

namespace TODO.BL.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly IGenericRepository<TodoEntity> _todoRepo;

        public TodoService(IGenericRepository<TodoEntity> todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public async Task<Guid> CreateTodo(TodoEntity entity)
        {
            return await _todoRepo.Add(entity);
        }

        public async Task<bool> DeleteTodo(Guid id)
        {
            if(await _todoRepo.DeleteById(id))
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<TodoEntity>> GetAllTodoAsync()
        {
            return await _todoRepo.GetAll();
        }

        public async Task<TodoEntity> GetTodoById(Guid id)
        {
            return await _todoRepo.GetById(id);
        }

        public async Task<bool> UpdateTodo(TodoEntity entity)
        {
            return await _todoRepo.Update(entity);
        }
    }
}
