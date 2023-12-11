using Newtonsoft.Json;

namespace Wx3rdApi.Models.Wx3rd
{
    public class BaseResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }
    }
}
