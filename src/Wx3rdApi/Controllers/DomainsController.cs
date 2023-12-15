using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 域名管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWxService _wxService;

        public DomainsController(IAuthService authService, IWxService wxService)
        {
            _authService = authService;
            _wxService = wxService;
        }

        /// <summary>
        /// 获取第三方平台服务器域名：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/thirdparty-management/domain-mgnt/modifyThirdpartyServerDomain.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("3rd/server")]
        public async Task<IActionResult> GetThirdpartyServerDomain()
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var modifyThirdpartyServerDomain = await _wxService.ModifyThirdpartyServerDomain(loginInfo.ComponentAccessToken, "get", false, string.Empty);
            if (modifyThirdpartyServerDomain.errcode != 0)
            {
                return BadRequest(modifyThirdpartyServerDomain);
            }

            return Ok(modifyThirdpartyServerDomain);
        }

        /// <summary>
        /// 获取第三方平台业务域名：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/thirdparty-management/domain-mgnt/modifyThirdpartyJumpDomain.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("3rd/jump")]
        public async Task<IActionResult> GetThirdpartyJumpDomain()
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var modifyThirdpartyJumpDomain = await _wxService.ModifyThirdpartyJumpDomain(loginInfo.ComponentAccessToken, "get", false, string.Empty);
            if (modifyThirdpartyJumpDomain.errcode != 0)
            {
                return BadRequest(modifyThirdpartyJumpDomain);
            }

            return Ok(modifyThirdpartyJumpDomain);
        }
    }
}
