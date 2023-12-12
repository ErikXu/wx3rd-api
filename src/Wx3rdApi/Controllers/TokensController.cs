using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Wx3rdApi.Models.Wx3rd;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 凭证管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IAuthService _authService;

        public TokensController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// 获取 component_access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet("component-access-token")]
        public IActionResult GetComponentAccessToken()
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            return Ok(loginInfo.ComponentAccessToken);
        }
    }
}
