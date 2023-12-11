using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;
        private readonly IWx3rdService _wx3rdService;
        private readonly IWxService _wxService;

        public TemplatesController(IMemoryCache cache, IWx3rdService wx3rdService, IWxService wxService)
        {
            _cache = cache;
            _wx3rdService = wx3rdService;
            _wxService = wxService;
        }

        /// <summary>
        /// 获取代码草稿列表：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/code_template/gettemplatedraftlist.html
        /// </summary>
        /// <returns></returns>
        [HttpGet("draft")]
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
                if (getComponentAccessTokenResponse.code != 0)
                {
                    return BadRequest(getComponentAccessTokenResponse);
                }
                else
                {
                    var listDraftResponse = await _wxService.ListDraft(getComponentAccessTokenResponse.data.token);
                    if (listDraftResponse.errcode != 0)
                    {
                        return BadRequest(listDraftResponse);
                    }
                    else
                    {
                        return Ok(listDraftResponse);
                    }
                }
            }

            return Unauthorized();
        }
    }
}
