using BlazeIt.Server.Repositories.FeedbackRepo;
using BlazeIt.Shared.DTOs.Request.Feedback;
using BlazeIt.Shared.DTOs.Response.Feedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazeIt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepo;
        public FeedbackController(IFeedbackRepository feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }


        [HttpPost]
        [Route("CreateFeedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
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

            var response = await _feedbackRepo.CreateFeedback(userId, request);
            if (!response.Successful)
            {
                return BadRequest(new CreateFeedbackResponse
                {
                    Successful = false,
                    Error = response.Error
                });
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("GetAllFeedback")]
        public async Task<IActionResult> GetAllFeedback()
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

            var response = await _feedbackRepo.GetAllFeedback(userId);
            return Ok(response);
        }


        [HttpPost]
        [Route("FeedbackVote")]
        public async Task<IActionResult> FeedbackVote([FromBody] FeedbackVoteRequest request)
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

            var response = await _feedbackRepo.FeedbackVote(userId, request);
            if (!response.Successful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
