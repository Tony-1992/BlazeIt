using BlazeIt.Server.Data;
using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using BlazeIt.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazeIt.Server.Repositories.AuthRepo
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        public AuthenticationRepository(IConfiguration config, ApplicationDbContext db)
        {
            _configuration = config;
            _db = db;
        }


        public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest model)
        {
            try
            {
                // Check if user exists in the db and retrieve details
                var userObj = await _db.TABLE_Users.FirstOrDefaultAsync(user => user.Email == model.Email && user.Password == model.Password);

                if (userObj == null)
                {
                    return new AuthenticationResponse
                    {
                        Successful = false,
                        Error = "User not found"
                    };
                }

                // pass user to token generator to use in claim
                var token = GenerateJwtToken(userObj);

                // create response with token included
                var response = new AuthenticationResponse
                {
                    Successful = true,
                    Token = token,
                };

                return response;
            }
            catch (Exception e)
            {
                return new AuthenticationResponse
                {
                    Successful = false,
                    Error = e.Message
                };
            }
        }

        public string GenerateJwtToken(User user)
        {
            // Get security key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SECRET_KEY"]));

            // Create signing credentials with the key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create claims the Token should hold about the user
            var claims = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                });

            // Create token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create token descriptor 
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["ISSUER"],
                Audience = _configuration["AUDIENCE"],
                Subject = claims,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = credentials,
            };

            // Create token in raw form
            var token = tokenHandler.CreateToken(tokenDescription);

            // Return serialized format token
            return tokenHandler.WriteToken(token);
        }
    }
}
