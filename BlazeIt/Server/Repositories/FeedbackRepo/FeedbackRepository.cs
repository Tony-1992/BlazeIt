using AutoMapper;
using BlazeIt.Server.Data;
using BlazeIt.Shared.DTOs.Request.Feedback;
using BlazeIt.Shared.DTOs.Response.Feedback;
using BlazeIt.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazeIt.Server.Repositories.FeedbackRepo
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FeedbackRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CreateFeedbackResponse> CreateFeedback(string userId, CreateFeedbackRequest model)
        {
            // Get user 
            var userObj = _db.TABLE_Users.FirstOrDefault(x => x.Id == userId);


            // map DTO to entity
            var feedbackObj = _mapper.Map<Feedback>(model);
            feedbackObj.CreatedById = userId;
            feedbackObj.Votes++;


            // Add feedback to users VotedOn
            userObj.VotedOn.Add(feedbackObj);

            // Add user to feedback voters
            feedbackObj.Voters.Add(userObj);

            // Add feedback to table
            _db.TABLE_Feedbacks.Add(feedbackObj);

            // Update
            var successful = await Save();


            if (!successful)
            {
                return new CreateFeedbackResponse
                {
                    Successful = false,
                    Error = "Unable to create feedback item."
                };

            }

            return new CreateFeedbackResponse
            {
                Successful = true,
            };
        }

        public async Task<List<FeedbackResponse>> GetAllFeedback(string userId)
        {
            try
            {

                var entityList = await _db.TABLE_Feedbacks.ToListAsync();
                List<FeedbackResponse> result = new List<FeedbackResponse>();

                foreach (var fbEntity in entityList)
                {
                    FeedbackResponse dto = _mapper.Map<FeedbackResponse>(fbEntity);
                    result.Add(dto);
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("hit");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<FeedbackVoteResponse> FeedbackVote(string userId, FeedbackVoteRequest model)
        {
            // Get user 
            var userObj = _db.TABLE_Users.FirstOrDefault(x => x.Id == userId);

            // Get Feedback
            var feedbackObj = _db.TABLE_Feedbacks.Include(x => x.Voters).FirstOrDefault(x => x.Id == model.FeedbackId);

            foreach (var voter in feedbackObj.Voters)
            {
                if (voter.Id == userId)
                {
                    return new FeedbackVoteResponse
                    {
                        Successful = false,
                        Error = "User already voted!"
                    };
                }

            }

            // increment count on feedback
            feedbackObj.Votes++;

            // add userID to voters
            feedbackObj.Voters.Add(userObj);

            // add feedback to user.votedOn
            userObj.VotedOn.Add(feedbackObj);

            // save
            await Save();

            return new FeedbackVoteResponse
            {
                Successful = true
            };
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
