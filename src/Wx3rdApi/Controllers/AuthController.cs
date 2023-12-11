using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using Wx3rdApi.Models.Wx3rd;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IWx3rdService _wx3rdService;
        public AuthController(IMemoryCache cache, IWx3rdService wx3rdService)
        {
            _cache = cache;
            _wx3rdService = wx3rdService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginForm form)
        {
            var loginInfo = new LoginInfo
            {
                Host = form.Host,
                Username = form.Username,
                Password = form.Password,
                PasswordMd5 = form.PasswordMd5
            };

            if (string.IsNullOrWhiteSpace(loginInfo.PasswordMd5))
            {
                loginInfo.PasswordMd5 = CreateMd5(loginInfo.Password);
            }

            var loginRequest = new LoginRequest
            {
                Username = loginInfo.Username,
                Password = loginInfo.PasswordMd5
            };

            var loginResponse = await _wx3rdService.Login(loginInfo.Host, loginRequest);
            if (loginResponse.Code != 0)
            {
                return BadRequest(loginResponse);
            }

            loginInfo.Jwt = loginResponse.Data.Jwt;

            var uuid = Guid.NewGuid().ToString();
            var exp = DateTimeOffset.UtcNow.AddMinutes(20);
            _cache.Set(uuid, loginInfo, exp);
            RefreshCookies("uuid", exp, uuid);
            return Ok();
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            if (Request.Cookies.TryGetValue("uuid", out var uuid))
            {
                var loginInfo = _cache.Get<LoginInfo>(uuid);
                return Ok(loginInfo);
            }

            return Unauthorized();
        }

        private static string CreateMd5(string input)
        {
            using MD5 md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }

        private void RefreshCookies(string key, DateTimeOffset exp, string value)
        {
            var options = new CookieOptions
            {
                Expires = exp,
                HttpOnly = true,
                Path = "/"
            };

            Response.Cookies.Append(key, value, options);
        }
    }
}
