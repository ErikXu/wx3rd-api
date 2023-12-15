using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Models.Wx3rd;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 凭证管理
    /// </summary>
    [Route("api/tokens")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWx3rdService _wx3rdService;

        public TokensController(IAuthService authService, IWx3rdService wx3rdService)
        {
            _authService = authService;
            _wx3rdService = wx3rdService;
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

        /// <summary>
        /// 获取 authorizer_access_token
        /// </summary>
        /// <returns></returns>
        [HttpGet("authorizer-access-token")]
        public async Task<IActionResult> GetAuthorizerAccessToken([FromQuery] GetAuthorizerAccessTokenForm form)
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

            return Ok(getAuthorizerAccessTokenResponse);
        }
    }
}
