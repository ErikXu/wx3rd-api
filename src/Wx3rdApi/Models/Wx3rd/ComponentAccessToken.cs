using Newtonsoft.Json;

namespace Wx3rdApi.Models.Wx3rd
{
    public class GetComponentAccessTokenResponse: BaseResponse
    {
        [JsonProperty("data")]
        public GetComponentAccessTokenData Data { get; set; }
    }

    public class GetComponentAccessTokenData
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
