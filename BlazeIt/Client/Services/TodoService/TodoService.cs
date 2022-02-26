using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using System.Net.Http.Json;

namespace BlazeIt.Client.Services.TodoService
{
    public class TodoService
    {
        // Delegates
        public event Action TodoCreated;
        public event Action TodoDeleted;
        public event Action TodoUpdated;

        private readonly HttpClient _httpClient;

        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Events
        public virtual void OnTodoCreated()
        {
            TodoCreated.Invoke();
        }

        public virtual void OnTodoDeleted()
        {
            TodoDeleted.Invoke();
        }

        public virtual void OnTodoUpdated()
        {
            TodoUpdated.Invoke();
        }


        // Requests to API
        public async Task<CreateTodoResponse> CreateTodo(CreateTodoRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/todo/CreateTodo", model);
            var data = await response.Content.ReadFromJsonAsync<CreateTodoResponse>();

            OnTodoCreated();
            return data;
        }

        public async Task<List<TodoResponse>> GetAllTodos()
        {
            var data = await _httpClient.GetFromJsonAsync<List<TodoResponse>>("api/todo/GetTodos");
            return data;
        }

        public async Task<DeleteTodoResponse> DeleteTodo(DeleteTodoRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Todo/DeleteTodo", model);
            var data = await response.Content.ReadFromJsonAsync<DeleteTodoResponse>();

            if (!data.Successful)
            {
                return data;
            }

            OnTodoDeleted();
            return data;
        }

        public async Task<UpdateTodoResponse> UpdateTodo(UpdateTodoRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Todo/UpdateTodo", model);
            var data = await response.Content.ReadFromJsonAsync<UpdateTodoResponse>();

            if (!data.Successful)
            {
                return data;
            }

            OnTodoUpdated();
            return data;
        }
    }
}
