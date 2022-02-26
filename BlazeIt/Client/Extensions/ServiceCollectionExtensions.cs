using BlazeIt.Client.Services;
using BlazeIt.Client.Services.AuthService;
using BlazeIt.Client.Services.FeedbackService;
using BlazeIt.Client.Services.TodoService;
using BlazeIt.Client.Services.UserService;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazeIt.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAuthenticationStateProviderServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            return services;
        }

        public static IServiceCollection RegisterClientServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationService, AuthenticationService>();
            services.AddScoped<UserService, UserService>();
            services.AddScoped<TodoService, TodoService>();
            services.AddScoped<FeedbackService, FeedbackService>();
            services.AddScoped<NotificationService, NotificationService>();
            return services;
        }
    }
}
