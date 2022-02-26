using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;

namespace BlazeIt.Server.Repositories.TodoRepo
{
    public interface ITodoRepository
    {
        public Task<List<TodoResponse>> GetUserTodos(string userId);
        public Task<CreateTodoResponse> CreateTodo(string userId, CreateTodoRequest model);
        public Task<DeleteTodoResponse> DeleteTodo(string userId, DeleteTodoRequest model);
        public Task<UpdateTodoResponse> UpdateTodo(string userId, UpdateTodoRequest model);
        public Task<bool> Save();
    }
}
