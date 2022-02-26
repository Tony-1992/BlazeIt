using BlazeIt.Shared.DTOs.Request.Feedback;
using BlazeIt.Shared.DTOs.Response.Feedback;

namespace BlazeIt.Server.Repositories.FeedbackRepo
{
    public interface IFeedbackRepository
    {
        public Task<List<FeedbackResponse>> GetAllFeedback(string userId);
        public Task<CreateFeedbackResponse> CreateFeedback(string userId, CreateFeedbackRequest model);
        //public Task<DeleteTodoResponse> DeleteFeedback(string userId, DeleteTodoRequest model);
        public Task<FeedbackVoteResponse> FeedbackVote(string userId, FeedbackVoteRequest model);
        public Task<bool> Save();
    }
}
