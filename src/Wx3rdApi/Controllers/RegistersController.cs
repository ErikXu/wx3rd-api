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

        /// <summary>
        /// 查询创建企业小程序任务状态：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/register-management/fast-registration-ent/registerMiniprogram.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchRegisterMiniProgram([FromQuery] SearchRegisterMiniProgramForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var searchRegisterMiniProgramRequest = new SearchRegisterMiniProgramRequest
            {
                name = form.Name,
                legal_persona_wechat = form.LegalPersonaWechat,
                legal_persona_name = form.LegalPersonaName
            };

            var searchRegisterMiniProgramResponse = await _wxService.SearchRegisterMiniProgram(loginInfo.ComponentAccessToken, searchRegisterMiniProgramRequest);
            if (searchRegisterMiniProgramResponse.errcode != 0)
            {
                return BadRequest(searchRegisterMiniProgramResponse);
            }
            else
            {
                return Ok(searchRegisterMiniProgramResponse);
            }
        }

        /// <summary>
        /// 快速注册个人小程序：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/register-management/fast-registration-ind/fastRegisterPersonalMp.html
        /// </summary>
        /// <returns></returns>
        [HttpPost("personal")]
        public async Task<IActionResult> RegisterPersonalMiniProgram([FromQuery] RegisterPersonalMiniProgramForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var registerPersonalMiniProgramRequest = new RegisterPersonalMiniProgramRequest
            {
                idname = form.IdName,
                wxuser = form.WxUser,
                component_phone = form.ComponentPhone
            };

            var registerPersonalMiniProgram = await _wxService.RegisterPersonalMiniProgram(loginInfo.ComponentAccessToken, registerPersonalMiniProgramRequest);
            if (registerPersonalMiniProgram.errcode != 0)
            {
                return BadRequest(registerPersonalMiniProgram);
            }
            else
            {
                return Ok(registerPersonalMiniProgram);
            }
        }

        /// <summary>
        /// 查询创建个人小程序任务状态：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/register-management/fast-registration-ind/fastRegisterPersonalMp.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("personal")]
        public async Task<IActionResult> SearchRegisterPersonalMiniProgram([FromQuery] SearchRegisterPersonalMiniProgramForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var searchRegisterPersonalMiniProgramRequest = new SearchRegisterPersonalMiniProgramRequest
            {
                taskid = form.TaskId
            };

            var searchRegisterPersonalMiniProgramResponse = await _wxService.SearchRegisterPersonalMiniProgram(loginInfo.ComponentAccessToken, searchRegisterPersonalMiniProgramRequest);
            if (searchRegisterPersonalMiniProgramResponse.errcode != 0)
            {
                return BadRequest(searchRegisterPersonalMiniProgramResponse);
            }
            else
            {
                return Ok(searchRegisterPersonalMiniProgramResponse);
            }
        }
    }
}
