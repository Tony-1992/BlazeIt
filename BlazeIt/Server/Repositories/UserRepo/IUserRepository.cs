using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;

namespace BlazeIt.Server.Repositories.UserRepo
{
    public interface IUserRepository
    {
        public Task<RegisterUserResponse> RegisterUser(RegisterUserRequest model);
        public Task<ChangePasswordResponse> ChangePassword(string id, ChangePasswordRequest model);
        public Task<DeleteAccountResponse> DeleteAccount(string id, DeleteAccountRequest model);
        public Task<UpdatePasswordResponse> UpdateGeneralDetails(string id, UpdateGeneralDetailsRequest model);
        public Task<bool> DoesUserExist(string email);
        public Task<GetUserEmailResponse> GetUserEmail(string userId);
        public Task<bool> Save();
    }
}
