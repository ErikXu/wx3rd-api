using Newtonsoft.Json;
using RestSharp;
using Wx3rdApi.Models.Wx3rd;

namespace Wx3rdApi.Services
{
    public interface IWx3rdService
    {
        Task<LoginResponse> Login(string host, LoginRequest loginRequest);

        Task<GetComponentAccessTokenResponse> GetComponentAccessToken(string host, string jwt);
    }

    public class Wx3rdService: IWx3rdService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly int _maxTimeout = 30000;

        public Wx3rdService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<LoginResponse> Login(string host, LoginRequest loginRequest)
        {
            var url = $"{host}/wxcomponent/auth";
            var request = new RestRequest(url);

            var body = JsonConvert.SerializeObject(loginRequest);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Put);

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
            if (baseResponse.Code == 0)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                return loginResponse;
            }
            else
            {
                return new LoginResponse
                {
                    Code = baseResponse.Code,
                    ErrorMsg = baseResponse.ErrorMsg
                };
            }
        }

        public async Task<GetComponentAccessTokenResponse> GetComponentAccessToken(string host, string jwt)
        {
            var url = $"{host}/wxcomponent/admin/component-access-token";
            var request = new RestRequest(url);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));

            using var restClient = new RestClient(new RestClientOptions { MaxTimeout = 30000 });
            var response = await restClient.GetAsync(request);

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
            if (baseResponse.Code == 0)
            {
                var getComponentAccessTokenResponse = JsonConvert.DeserializeObject<GetComponentAccessTokenResponse>(response.Content);
                return getComponentAccessTokenResponse;
            }
            else
            {
                return new GetComponentAccessTokenResponse
                {
                    Code = baseResponse.Code,
                    ErrorMsg = baseResponse.ErrorMsg
                };
            }
        }
    }
}
