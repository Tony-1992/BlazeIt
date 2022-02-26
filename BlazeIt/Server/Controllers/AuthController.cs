using BlazeIt.Server.Repositories.AuthRepo;
using BlazeIt.Shared.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace BlazeIt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepository;
        public AuthController(IAuthenticationRepository authRepo)
        {
            _authRepository = authRepo;
        }


        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticationRequest model)
        {
            // Handle invalid models


            // Authenticate user and generate jwt token to return
            var response = await _authRepository.AuthenticateUser(model);

            // if unsuccessful, user does not exist or password is incorrect
            // handle more cases
            if (!response.Successful)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
