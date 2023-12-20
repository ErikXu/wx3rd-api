using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Models.Wx;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 代商家注册小程序
    /// </summary>
    [Route("api/registers")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWxService _wxService;

        public RegistersController(IAuthService authService, IWxService wxService)
        {
            _authService = authService;
            _wxService = wxService;
        }

        /// <summary>
        /// 快速注册企业小程序：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/register-management/fast-registration-ent/registerMiniprogram.html
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterMiniProgram([FromQuery] RegisterMiniProgramForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var registerMiniProgramRequest = new RegisterMiniProgramRequest
            {
                name = form.Name,
                code = form.Code,
                code_type = form.CodeType,
                legal_persona_wechat = form.LegalPersonaWechat,
                legal_persona_name = form.LegalPersonaName,
                component_phone = form.ComponentPhone
            };

            var registerMiniProgramResponse = await _wxService.RegisterMiniProgram(loginInfo.ComponentAccessToken, registerMiniProgramRequest);
            if (registerMiniProgramResponse.errcode != 0)
            {
                return BadRequest(registerMiniProgramResponse);
            }
            else
            {
                return Ok(registerMiniProgramResponse);
            }
        }
    }
}
