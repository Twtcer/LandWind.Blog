using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using LandWind.Blog.Core.DataAnnotation.Output;
using LandWind.Blog.Core.Dto.Users;
using LandWind.Blog.Core.Extensions; 
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace LandWind.Blog.Admin.Services
{
    public class OAuthService : AuthenticationStateProvider
    {
        private readonly HttpClient http;
        private readonly IJSRuntime _jsRuntime;
        private NavigationManager _navigationManager;

        public OAuthService(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            http = httpClientFactory.CreateClient("api");
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            if (string.IsNullOrEmpty(token))
            {
                return GetNullState();
            }
            else
            {
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var httpResponse = await http.GetAsync("api/user");
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return GetNullState();
                }

                var response = await httpResponse.Content.ReadAsStringAsync();
                var user = response.Deserialize<UserDto>();

                if (user is null)
                {
                    return GetNullState();
                }

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("avatar", user.Avatar),
                }, "landwind.blog oauth");

                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }

        }

        public async Task GetOAuthUrl(string type)
        {
            var json = await http.GetStringAsync($"/api/oauth/{type}");
            var response = json.Deserialize<IResponseOutput<string>>();

            _navigationManager.NavigateTo(response.Success ? response.Data : "/Login");
        }

        private AuthenticationState GetNullState()
        {
            _navigationManager.NavigateTo("/login");

            var principal = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(principal);
        }
    }
}
