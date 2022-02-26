using BlazeIt.Server.Data;
using BlazeIt.Server.Repositories.AuthRepo;
using BlazeIt.Server.Repositories.FeedbackRepo;
using BlazeIt.Server.Repositories.TodoRepo;
using BlazeIt.Server.Repositories.UserRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BlazeIt.Server.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            return services;
        }

        public static IServiceCollection RegisterDBServices(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
            return services;
        }


        public static IServiceCollection RegisterMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IServiceCollection RegisterJwtAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            var secret = Encoding.ASCII.GetBytes(config.GetJwtSecretKey());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                // Tell authentication middleware what to validate the jwt against
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = config["ISSUER"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = config["AUDIENCE"],

                    // Validate the token expiry (default 5 minutes)
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here:
                    ClockSkew = TimeSpan.FromMinutes(1),
                };
            });

            return services;
        }

        public static IServiceCollection RegisterSwaggerDocsServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // https://www.freecodespot.com/blog/use-jwt-bearer-authorization-in-swagger/
                // Define swagger authentication definition
                var securityDefinition = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Description = "JWT authorisation header using the bearer scheme."
                };

                // Define swagger authentication requirements
                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        new List<string>()
                    }
                };

                // Define swagger API docs below: 
                // v1
                var swaggerDocsV1 = new OpenApiInfo
                {
                    Title = "BlazeIt API",
                    Version = "v1",
                };



                // Add swagger versions below
                options.SwaggerDoc("v1", swaggerDocsV1);

                // Add Security requirements/definition
                options.AddSecurityRequirement(securityRequirements);
                options.AddSecurityDefinition("bearerAuth", securityDefinition);

            });

            return services;
        }
    }
}
