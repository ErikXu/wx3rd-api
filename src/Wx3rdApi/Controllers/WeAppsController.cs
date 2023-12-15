using Microsoft.AspNetCore.Mvc;
using Wx3rdApi.Models.Wx3rd;
using Wx3rdApi.Services;

namespace Wx3rdApi.Controllers
{
    /// <summary>
    /// 代开发小程序管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeAppsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWx3rdService _wx3rdService;

        public WeAppsController(IAuthService authService, IWx3rdService wx3rdService)
        {
            _authService = authService;
            _wx3rdService = wx3rdService;
        }

        /// <summary>
        /// 获取代开发小程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWeApps([FromQuery] ListWeAppForm form)
        {
            if (form.Offset < 0)
            {
                form.Offset = 0;
            }

            if (form.Limit < 1)
            {
                form.Limit = 1;
            }

            var loginInfo = _authService.GetLoginInfo(Request);
            if (loginInfo == null)
            {
                return Unauthorized();
            }

            var listWeAppResponse = await _wx3rdService.ListWeApp(loginInfo.Host, loginInfo.Jwt, form.Offset, form.Limit);
            if (listWeAppResponse.code != 0)
            {
                return BadRequest(listWeAppResponse);
            }

            return Ok(listWeAppResponse);
        }
    }
}
