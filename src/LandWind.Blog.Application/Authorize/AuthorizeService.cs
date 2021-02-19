using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LandWind.Blog.Common.Github;
using LandWind.Blog.Domain.Configurations;
using LandWind.Blog.Domain.Shared.Base;

namespace LandWind.Blog.Application.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthorizeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseResult<string>> GetLoginAddressAsync()
        {
            var result = new ResponseResult<string>();

            var req = new AuthorizeRequest();
            var address = string.Concat(new string[]{
                 GithubConfig.ApiAuthorizeUrl,
                    "?client_id=", req.ClientId,
                    "&scope=", req.Scope,
                    "&state=", req.State,
                    "&redirect_uri=", req.RedirectUri
            });
            result.IsSuccess(address);

            return await Task.FromResult(result);
        }
        public async Task<ResponseResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ResponseResult<string>();

            if (string.IsNullOrEmpty(code))
            {
                result.IsFailed("code为空");
                return result;
            }

            var request = new AccessTokenRequest();
            var content = new StringContent($"code={code}&client_id={request.ClientId}&redirect_uri={request.RedirectUri}&client_secret={request.ClientSecret}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using var client = _httpClientFactory.CreateClient();
            var httpResponse = await client.PostAsync(GithubConfig.ApiAccessTokenUrl, content);
            var response = await httpResponse.Content.ReadAsStringAsync();

            if (response.StartsWith("access_token"))
            {
                result.IsSuccess(response.Split("=")[1].Split("&").First());
            }
            else
            {
                result.IsFailed("code不正确");
            }

            return result;
        }
        public Task<ResponseResult<string>> GenerateTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

    }
}
