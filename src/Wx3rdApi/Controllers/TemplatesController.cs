using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Models.Wx;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 小程序模板库管理：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/operation/thirdparty/template.html
    /// </summary>
    [Route("api/templates")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWxService _wxService;

        public TemplatesController(IAuthService authService, IWxService wxService)
        {
            _authService = authService;
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
        /// 将草稿添加到代码模板库：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/code_template/addtotemplate.html
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTemplate([FromQuery] AddTemplateForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var addTemplateResponse = await _wxService.AddTemplate(loginInfo.ComponentAccessToken, form.DraftId, form.TemplateType);
            if (addTemplateResponse.errcode != 0)
            {
                return BadRequest(addTemplateResponse);
            }
            else
            {
                return Ok(addTemplateResponse);
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

        /// <summary>
        /// 删除指定代码模板：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/code_template/deletetemplate.html
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTemplate([FromQuery] DeleteTemplateForm form)
        {
            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var deleteTemplateResponse = await _wxService.DeleteTemplate(loginInfo.ComponentAccessToken, form.TemplateId);
            if (deleteTemplateResponse.errcode != 0)
            {
                return BadRequest(deleteTemplateResponse);
            }
            else
            {
                return Ok(deleteTemplateResponse);
            }
        }
    }
}
