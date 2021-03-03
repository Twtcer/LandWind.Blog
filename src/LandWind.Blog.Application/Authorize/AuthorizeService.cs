using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using LandWind.Blog.Application.Caching.Authorize;
using LandWind.Blog.Core.Auth.Github;
using LandWind.Blog.Core.Domain.Options;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Response.Base;
using LandWind.Blog.Domain.Configurations; 
using Microsoft.IdentityModel.Tokens;

namespace LandWind.Blog.Application.Authorize
{
    public class AuthorizeService : LandWindBlogAppServiceBase, IAuthorizeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthorizeCacheService _authorizeCacheService;
        public AuthorizeService(IHttpClientFactory httpClientFactory, IAuthorizeCacheService authorizeCacheService)
        {
            _httpClientFactory = httpClientFactory;
            _authorizeCacheService = authorizeCacheService;
        }

        /// <summary>
        /// 获取登录地址
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeCacheService.GetLoginAddressAsync(async () =>
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
            });
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> GetAccessTokenAsync(string code)
        {
            var result = new ResponseResult<string>();

            if (string.IsNullOrEmpty(code))
            {
                result.IsFailed("code为空");
                return result;
            }

            return await _authorizeCacheService.GetAccessTokenAsync(code, async () =>
            {
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
            });
        }
        public async Task<ResponseResult<string>> GenerateTokenAsync(string accessToken)
        {
            var result = new ResponseResult<string>();
            if (string.IsNullOrEmpty(accessToken))
            {
                result.IsFailed("access_token 为空");
                return result;
            }

            return await _authorizeCacheService.GenerateTokenAsync(accessToken, async () =>
            {
                var url = $"{GithubConfig.ApiUserUrl}?access_token={accessToken}";
                using var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36 Edg/88.0.705.74");
                client.DefaultRequestHeaders.Add("Authorization",$"token {accessToken}");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    result.IsFailed("access_token不正确");
                    return result;
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var user = content.DeserializeToObject<UserResponse>();
                if (user == null)
                {
                    result.IsFailed("为获取到用户数据");
                    return result;
                }

                if (user.Id != GithubConfig.UserId)
                {
                    result.IsFailed("当前账号未授权");
                    return result;
                }

                var claims = new[] {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(Appsettings.JWT.Expires)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            };

                var key = new SymmetricSecurityKey(Appsettings.JWT.SecurityKey.ToUtf8());
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    issuer: Appsettings.JWT.Domain,
                    audience: Appsettings.JWT.Domain,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Appsettings.JWT.Expires),
                    signingCredentials: creds
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                result.IsSuccess(token);

                return await Task.FromResult(result);
            });

        }
    }
}
