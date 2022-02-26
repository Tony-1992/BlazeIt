using BlazeIt.Server.Repositories.UserRepo;
using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazeIt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        [HttpPost]
        [Route("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            // Handle poor requests

            RegisterUserResponse response = await _userRepo.RegisterUser(request);
            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountRequest request)
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

            var response = await _userRepo.DeleteAccount(userId, request);

            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
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

            var response = await _userRepo.ChangePassword(userId, request);

            return Ok(response);
        }


        [HttpPut]
        [Route("UpdateGeneralDetails")]
        public async Task<IActionResult> UpdateGeneralDetails([FromBody] UpdateGeneralDetailsRequest request)
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

            var response = await _userRepo.UpdateGeneralDetails(userId, request);
            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("GetUserEmail")]
        public async Task<IActionResult> GetUserEmail()
        {
            // https://www.examplefiles.net/cs/159575

            // Cast to ClaimsIdentity.
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            IEnumerable<Claim> claim = identity.Claims;

            // Gets ID from claims.
            var userId = claim
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()
                .Value;


            GetUserEmailResponse response = await _userRepo.GetUserEmail(userId);

            return Ok(response);
        }
    }
}
