using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Wx3rdApi.Models.Wx3rd
{
    public class LoginForm
    {
        /// <summary>
        /// 第三方平台域名，示例：https://wxcomponent-xxx.sh.run.tcloudbase.com
        /// </summary>
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// 第三方平台用户名，示例：root
        /// </summary>
        [Required]
        [DefaultValue("root")]
        public string Username { get; set; }

        /// <summary>
        /// 第三方平台密码，和第三方平台密码 MD5 二选一
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// [建议] 第三方平台密码 MD5，和第三方平台密码二选一，示例：e10adc3949ba59abbe56e057f20f883e
        /// </summary>
        public string PasswordMd5 { get; set; }
    }

    public class LoginInfo : LoginForm
    {
        public string Jwt { get; set; }
    }

    public class LoginRequest
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class LoginResponse: BaseResponse
    {
        public LoginData data { get; set; }
    }

    public class LoginData
    {
        public string jwt { get; set; }
    }
}
