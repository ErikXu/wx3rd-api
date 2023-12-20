using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Models.Wx;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWxService _wxService;
        private readonly IWx3rdService _wx3rdService;

        public InfoController(IAuthService authService, IWxService wxService, IWx3rdService wx3rdService)
        {
            _authService = authService;
            _wxService = wxService;
            _wx3rdService = wx3rdService;
        }

        /// <summary>
        /// 获取基本信息：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/basic-info-management/getAccountBasicInfo.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBasicInfo([FromQuery] GetBasicInfoForm form)
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

            var getAccountBasicInfo = await _wxService.GetAccountBasicInfo(getAuthorizerAccessTokenResponse.data.token);
            if (getAccountBasicInfo.errcode != 0)
            {
                return BadRequest(getAccountBasicInfo);
            }

            return Ok(getAccountBasicInfo);
        }
    }
}
