using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazeIt.Client.Services.AuthService
{
    public class AuthenticationService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authProvider;

        public AuthenticationService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authProvider = authProvider;
        }

        public async Task<AuthenticationResponse> AuthenticateDetils(AuthenticationRequest model)
        {
            // Jwt response
            var response = await _http.PostAsJsonAsync("api/Auth/verify", model);


            // Get token
            var data = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            // User not found, no token will be provided
            if (!data.Successful)
            {
                return data;
            }

            // Store in Local storage
            await _localStorage.SetItemAsStringAsync("authToken", data.Token);

            // Call to update Authstate
            (_authProvider as CustomAuthStateProvider).NotifyAuthChange();

            return data;
        }

        public async void Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            (_authProvider as CustomAuthStateProvider).NotifyAuthChange();
        }

    }
}
