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
}
