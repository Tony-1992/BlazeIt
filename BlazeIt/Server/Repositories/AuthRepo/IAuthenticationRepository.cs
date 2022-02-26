using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using BlazeIt.Shared.Entities;

namespace BlazeIt.Server.Repositories.AuthRepo
{
    public interface IAuthenticationRepository
    {
        public Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest model);
        public string GenerateJwtToken(User user);
    }
}
