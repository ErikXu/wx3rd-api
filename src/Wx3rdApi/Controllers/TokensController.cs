using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Wx3rdApi.Models.Wx3rd;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IWx3rdService _wx3rdService;
        public TokensController(IMemoryCache cache, IWx3rdService wx3rdService)
        {
            _cache = cache;
            _wx3rdService = wx3rdService;
        }

        /// <summary>
        /// 获取 component_access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet("component-access-token")]
        public async Task<IActionResult> GetComponentAccessToken()
        {
            if (Request.Cookies.TryGetValue("uuid", out var uuid))
            {
                var loginInfo = _cache.Get<LoginInfo>(uuid);
                if (loginInfo == null)
                {
                    return Unauthorized();
                }

                var getComponentAccessTokenResponse = await _wx3rdService.GetComponentAccessToken(loginInfo.Host, loginInfo.Jwt);
                if (getComponentAccessTokenResponse.Code != 0)
                {
                    return BadRequest(getComponentAccessTokenResponse);
                }
                else
                {
                    return Ok(getComponentAccessTokenResponse);
                }
            }

            return Unauthorized();
        }
    }
}
