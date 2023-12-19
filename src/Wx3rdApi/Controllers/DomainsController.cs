using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Models.Wx;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 域名管理
    /// </summary>
    [Route("api/domains")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWxService _wxService;
        private readonly IWx3rdService _wx3rdService;

        public DomainsController(IAuthService authService, IWxService wxService, IWx3rdService wx3rdService)
        {
            _authService = authService;
            _wxService = wxService;
            _wx3rdService = wx3rdService;
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

        /// <summary>
        /// 获取第三方平台业务域名校验文件：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/thirdparty-management/domain-mgnt/getThirdpartyJumpDomainConfirmFile.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("3rd/jump/file")]
        public async Task<IActionResult> GetThirdpartyJumpConfirmFile()
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var getThirdpartyDomainConfirmFileResponse = await _wxService.GetThirdpartyDomainConfirmFile(loginInfo.ComponentAccessToken);
            if (getThirdpartyDomainConfirmFileResponse.errcode != 0)
            {
                return BadRequest(getThirdpartyDomainConfirmFileResponse);
            }

            return Ok(getThirdpartyDomainConfirmFileResponse);
        }

        /// <summary>
        /// 获取发布后生效服务器域名列表：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getEffectiveServerDomain.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("app/server/effective")]
        public async Task<IActionResult> GetAppEffectiveServerDomain([FromQuery] GetAppDomainForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var getAuthorizerAccessTokenResponse = await _wx3rdService.GetAuthorizerAccessToken(loginInfo.Host, loginInfo.Jwt, form.AppId);
            if (getAuthorizerAccessTokenResponse.code != 0)
            {
                return BadRequest(getAuthorizerAccessTokenResponse);
            }

            var getAppEffectiveServerDomainResponse = await _wxService.GetAppEffectiveServerDomain(getAuthorizerAccessTokenResponse.data.token);
            if (getAppEffectiveServerDomainResponse.errcode != 0)
            {
                return BadRequest(getAppEffectiveServerDomainResponse);
            }

            return Ok(getAppEffectiveServerDomainResponse);
        }
    }
}
