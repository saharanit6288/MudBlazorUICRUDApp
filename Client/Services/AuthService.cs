using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazorUICRUDApp.Client.Services.Interface;
using MudBlazorUICRUDApp.Shared.AuthModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace MudBlazorUICRUDApp.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseResult> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);
            return await response.Content.ReadFromJsonAsync<AuthResponseResult>();
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/login",
                new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UserDetails> GetUserInfo()
        {
            return await _httpClient.GetFromJsonAsync<UserDetails>("api/accounts/GetUserInfo");
        }

        public async Task<AuthResponseResult> ChangePassword(ChangePasswordModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/ChangePassword", model);
            return await response.Content.ReadFromJsonAsync<AuthResponseResult>();
        }

        public async Task<AuthResponseResult> ResetPassword(ResetPasswordModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/ResetPassword", model);
            return await response.Content.ReadFromJsonAsync<AuthResponseResult>();
        }
    }
}
