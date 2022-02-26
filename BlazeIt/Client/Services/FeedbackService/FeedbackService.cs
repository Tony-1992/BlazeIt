using BlazeIt.Shared.DTOs.Request.Feedback;
using BlazeIt.Shared.DTOs.Response.Feedback;
using System.Net.Http.Json;

namespace BlazeIt.Client.Services.FeedbackService
{
    public class FeedbackService
    {
        // Delegates
        public event Action FeedbackCreated;
        public event Action FeedbackUpdated;


        private readonly HttpClient _httpClient;
        public FeedbackService(HttpClient http)
        {
            _httpClient = http;
        }

        // Events
        public void OnFeedbackCreated()
        {
            FeedbackCreated.Invoke();
        }
        public void OnFeedbackUpdated()
        {
            FeedbackUpdated.Invoke();
        }



        public async Task<CreateFeedbackResponse> CreateFeedback(CreateFeedbackRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/feedback/CreateFeedback", model);
            var data = await response.Content.ReadFromJsonAsync<CreateFeedbackResponse>();

            OnFeedbackCreated();
            return data;
        }

        public async Task<List<FeedbackResponse>> GetAllFeedback()
        {
            var data = await _httpClient.GetFromJsonAsync<List<FeedbackResponse>>("api/feedback/GetAllFeedback");
            return data;
        }

        public async Task FeedbackVote(FeedbackVoteRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/feedback/FeedbackVote", model);
            var data = await response.Content.ReadFromJsonAsync<FeedbackVoteResponse>();

            if (!data.Successful)
            {
                return;
            }

            OnFeedbackUpdated();
        }
    }
}
