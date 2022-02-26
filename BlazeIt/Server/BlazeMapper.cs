using AutoMapper;
using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Request.Feedback;
using BlazeIt.Shared.DTOs.Response;
using BlazeIt.Shared.DTOs.Response.Feedback;
using BlazeIt.Shared.Entities;

namespace BlazeIt.Server
{
    public class BlazeMapper : Profile
    {
        public BlazeMapper()
        {
            // User Mapping
            CreateMap<User, RegisterUserRequest>().ReverseMap();
            CreateMap<User, UpdateGeneralDetailsRequest>().ReverseMap();
            CreateMap<User, ChangePasswordRequest>().ReverseMap();


            // Todo Mapping
            CreateMap<Todo, CreateTodoRequest>().ReverseMap();
            CreateMap<Todo, TodoResponse>().ReverseMap();
            CreateMap<Todo, UpdateTodoRequest>().ReverseMap();


            // Feedback Mapping
            CreateMap<Feedback, CreateFeedbackRequest>().ReverseMap();
            CreateMap<Feedback, FeedbackResponse>().ReverseMap();

        }
    }
}
