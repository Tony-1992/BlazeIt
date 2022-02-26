using BlazeIt.Server.Repositories.TodoRepo;
using BlazeIt.Shared.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazeIt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepo;

        public TodoController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        [Route("GetTodos")]
        public async Task<IActionResult> GetTodos()
        {
            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims.
            var userId = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()
                .Value;

            var response = await _todoRepo.GetUserTodos(userId);
            return Ok(response);
        }


        [HttpPost]
        [Route("CreateTodo")]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request)
        {
            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims.
            var userId = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()
                .Value;

            var response = await _todoRepo.CreateTodo(userId, request);
            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("DeleteTodo")]
        public async Task<IActionResult> DeleteTodo([FromBody] DeleteTodoRequest request)
        {
            // Validate model

            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims.
            var userId = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()
                .Value;

            var response = await _todoRepo.DeleteTodo(userId, request);

            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("UpdateTodo")]
        public async Task<IActionResult> UpdateTodo([FromBody] UpdateTodoRequest request)
        {
            // Validate model

            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims.
            var userId = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()
                .Value;

            var response = await _todoRepo.UpdateTodo(userId, request);

            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
