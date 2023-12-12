using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Wx3rdApi.Models.Wx;
using Wx3rdApi.Models.Wx3rd;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 小程序模板库管理：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/operation/thirdparty/template.html
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWx3rdService _wx3rdService;
        private readonly IWxService _wxService;

        public TemplatesController(IAuthService authService, IWx3rdService wx3rdService, IWxService wxService)
        {
            _authService = authService;
            _wx3rdService = wx3rdService;
            _wxService = wxService;
        }

        /// <summary>
        /// 获取代码草稿列表：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/code_template/gettemplatedraftlist.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("draft")]
        public async Task<IActionResult> GetTemplateDrafts()
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var listDraftResponse = await _wxService.ListDraft(loginInfo.ComponentAccessToken);
            if (listDraftResponse.errcode != 0)
            {
                return BadRequest(listDraftResponse);
            }
            else
            {
                return Ok(listDraftResponse);
            }
        }

        /// <summary>
        /// 获取代码模板列表：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/code_template/gettemplatelist.html
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTemplates([FromQuery] ListTemplateForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var listTemplateResponse =await _wxService.ListTemplate(loginInfo.ComponentAccessToken, form.TemplateType);
            if (listTemplateResponse.errcode != 0)
            {
                return BadRequest(listTemplateResponse);
            }
            else
            {
                return Ok(listTemplateResponse);
            }
        }
    }
}
