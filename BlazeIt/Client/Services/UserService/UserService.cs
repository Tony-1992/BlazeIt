using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using System.Net.Http.Json;

namespace BlazeIt.Client.Services.UserService
{
    public class UserService
    {
        private readonly HttpClient _http;
        public UserService(HttpClient http)
        {
            _http = http;
        }


        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest user)
        {
            var response = await _http.PostAsJsonAsync("api/User/RegisterUser", user);

            var data = await response.Content.ReadFromJsonAsync<RegisterUserResponse>();

            return data;
        }

        public async Task<UpdatePasswordResponse> UpdateGeneralDetails(UpdateGeneralDetailsRequest user)
        {
            var response = await _http.PutAsJsonAsync("api/User/UpdateGeneralDetails", user);

            var data = await response.Content.ReadFromJsonAsync<UpdatePasswordResponse>();

            return data;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest details)
        {
            var response = await _http.PutAsJsonAsync("api/User/ChangePassword", details);

            var data = await response.Content.ReadFromJsonAsync<ChangePasswordResponse>();

            return data;

        }

        public async Task<DeleteAccountResponse> DeleteAccount(DeleteAccountRequest details)
        {
            var response = await _http.PostAsJsonAsync("api/User/DeleteAccount", details);

            var data = await response.Content.ReadFromJsonAsync<DeleteAccountResponse>();

            return data;

        }

        public async Task<GetUserEmailResponse> GetUserEmail()
        {
            var data = await _http.GetFromJsonAsync<GetUserEmailResponse>("api/User/GetUserEmail");

            return data;
        }
    }
}
