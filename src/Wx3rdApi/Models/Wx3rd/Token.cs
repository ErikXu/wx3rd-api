using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Wx3rdApi.Models.Wx3rd
{
    public class GetComponentAccessTokenResponse: BaseResponse
    {
        public GetComponentAccessTokenData data { get; set; }
    }

    public class GetComponentAccessTokenData
    {
        public string token { get; set; }
    }

    public class GetAuthorizerAccessTokenForm
    {
        /// <summary>
        /// 小程序 Id
        /// </summary>
        [Required]
        public string AppId { get; set; }
    }

    public class GetAuthorizerAccessTokenResponse : BaseResponse
    {
        public GetAuthorizerAccessTokenData data { get; set; }
    }

    public class GetAuthorizerAccessTokenData
    {
        public string token { get; set; }
    }
}
