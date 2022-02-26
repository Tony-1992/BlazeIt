using BlazeIt.Client.Utilities;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlazeIt.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }


        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // list of claims => ClaimsIdentity => ClaimsPrincipal => AuthenticationState

            // Get token from LS if exists
            var token = await _localStorage.GetItemAsStringAsync("authToken");

            // Return unauthorised claimPrincipal
            if (string.IsNullOrEmpty(token))
            {
                return SetNotAuthorisedUser();
            }

            //If token exists then create new identity with claims
            // Get all claims from Jwt
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var userIdentity = new ClaimsIdentity(claims, "authType");

            // Check expiryTime
            var expTimeToConv = claims
                .Where(x => x.Type == "exp")
                .FirstOrDefault()
                .Value;

            var tokenExpiry = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expTimeToConv)).UtcDateTime;

            // If expired, return unauthorised claimPrincipal
            if (tokenExpiry < DateTime.Now)
            {
                await _localStorage.RemoveItemAsync("authToken");
                return SetNotAuthorisedUser();
            }

            // Set jwt in header to be read server side
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(userIdentity));
        }

        public void NotifyAuthChange()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private AuthenticationState SetNotAuthorisedUser()
        {
            var anonymous = new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(anonymous));
        }
    }
}

