using AutoMapper;
using BlazeIt.Server.Data;
using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using BlazeIt.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazeIt.Server.Repositories.TodoRepo
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public TodoRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<TodoResponse>> GetUserTodos(string userId)
        {
            var allTodos = await _db.TABLE_Todos.Where(todo => todo.UserId == userId).ToListAsync();
            List<TodoResponse> result = new List<TodoResponse>();

            foreach (var todo in allTodos)
            {
                TodoResponse dto = _mapper.Map<TodoResponse>(todo);
                result.Add(dto);
            }

            return result;
        }


        public async Task<CreateTodoResponse> CreateTodo(string userId, CreateTodoRequest model)
        {
            // map DTO to entity
            var entityObj = _mapper.Map<Todo>(model);
            entityObj.UserId = userId;

            // add todo to db
            await _db.TABLE_Todos.AddAsync(entityObj);
            var successful = await Save();

            // Handle error
            if (!successful)
            {
                var badResponse = new CreateTodoResponse
                {
                    Successful = false,
                    Error = "Unable to create todo item"
                };

                return badResponse;
            }

            // update
            var response = new CreateTodoResponse
            {
                Successful = true
            };

            return response;
        }


        public async Task<DeleteTodoResponse> DeleteTodo(string userId, DeleteTodoRequest model)
        {
            // validate model

            // Current entityObj
            var entity = await _db.TABLE_Todos.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);


            _db.TABLE_Todos.Remove(entity);

            var successful = await Save();
            if (!successful)
            {
                return new DeleteTodoResponse
                {
                    Successful = false,
                    Error = "Unable to delete item"
                };
            }


            var response = new DeleteTodoResponse
            {
                Successful = true
            };

            return response;
        }

        public async Task<UpdateTodoResponse> UpdateTodo(string userId, UpdateTodoRequest model)
        {
            // validate model

            // Current entityObj
            var entity = await _db.TABLE_Todos.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);

            // Map
            entity = _mapper.Map(model, entity);

            _db.TABLE_Todos.Update(entity);

            var successful = await Save();
            if (!successful)
            {
                return new UpdateTodoResponse
                {
                    Successful = false,
                    Error = "Unable to update item"
                };
            }


            var response = new UpdateTodoResponse
            {
                Successful = true
            };

            return response;
        }


        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
