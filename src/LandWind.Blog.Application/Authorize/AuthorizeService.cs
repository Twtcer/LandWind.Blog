using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using LandWind.Blog.Application.Authorize.OAuth;
using LandWind.Blog.Application.Caching.Authorize;
using LandWind.Blog.Application.Tool;
using LandWind.Blog.Application.Users;
using LandWind.Blog.Core.Domain.Users;
using LandWind.Blog.Core.Dto.Authorize;
using LandWind.Blog.Core.Dto.Tools;
using LandWind.Blog.Core.Extensions;
using LandWind.Blog.Core.Options;
using LandWind.Blog.Core.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LandWind.Blog.Application.Authorize
{ 
    public class AuthorizeService : BlogAppServiceBase, IAuthorizeService
    {
        //private readonly Appsettings _appsettings;
        private readonly JwtOptions _jwtOptions;
        private readonly IToolService _toolService;
        private readonly IAuthorizeCacheService _authorizeCacheService;
        private readonly IUserService _userService;
        private readonly OAuthGithubService _githubService;

        public AuthorizeService(
                IOptions<JwtOptions> jwtOptions,
                IToolService toolService,
                IAuthorizeCacheService authorizeCacheService,
                IUserService userService,
                OAuthGithubService githubService
            )
        {
            _jwtOptions = jwtOptions.Value;
            _toolService = toolService;
            _authorizeCacheService = authorizeCacheService;
            _userService = userService;
            _githubService = githubService;
        }

        /// <summary>
        /// Get authorize url
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("api/oauth/{type}")]
        public async Task<ResponseResult<string>> GetAuthorizeUrlAsync(string type)
        {
            var state = StateManager.Instance.Get();
            var response = new ResponseResult<string>
            {
                Result = type switch
                {
                    "" => await _githubService.GetAuthorizeUrlAsync(state),
                    _ => throw new NotImplementedException($"Not implemented:{type}")
                }
            };

            return response;
        }

        public Task<ResponseResult<string>> GenerateTokenAsync([Required] string code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Generate token by <paramref name="type">
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/oauth/{type}/token")]
        public async Task<ResponseResult<string>> GenerateTokenAsync(string type, string code, string state)
        {
            var response = new ResponseResult<string>();
            if (!StateManager.IsExist(state))
            {
                response.IsFailed("Request failed");
                return response;
            }

            StateManager.Remove(state);
            var token = type switch
            {
                "github" => GenerateToken(await _githubService.GetUserByOAuthAsync(type, code, state)),
                _ => throw new NotImplementedException($"Not implemented {type}")
            };
            response.IsSuccess(token);

            return response;
        }

        /// <summary>
        /// Generate token by account.
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("api/oauth/account/token")]
        public async Task<ResponseResult<string>> GenerateTokenAsync([FromServices] IUserService userService, AccountInput input)
        {
            var response = new ResponseResult<string>();
            var user = await userService.VerifyByAccountAsync(input.Username, input.Password);
            var token = GenerateToken(user);
            response.IsSuccess(token);

            return await Task.FromResult(response);
        }

        /// <summary>
        /// Send authorization code
        /// </summary>
        /// <returns></returns>
        [Route("api/oauth/code/send")]
        public async Task<ResponseResult> SendAuthorizeCodeAsync()
        {
            var response = new ResponseResult();
            var length = 6;
            var code = length.GenerateRandomCode();
            await _authorizeCacheService.AddAuthorizeCodeAsync(code);
            await _toolService.SendMessageAsync(new SendMessageInput { 
                Text  =code
            });

            return response;
        }

        private string GenerateToken(User user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("avatar",user.Avatar),
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(_jwtOptions.Expires)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            };
            var key = new SymmetricSecurityKey(_jwtOptions.SigningKey.GetBytes());
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.Expires),
                signingCredentials: creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        } 
    }
}
