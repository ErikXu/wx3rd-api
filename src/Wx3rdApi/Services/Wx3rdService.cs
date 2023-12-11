using Newtonsoft.Json;
using RestSharp;
using Wx3rdApi.Models.Wx3rd;

namespace Wx3rdApi.Services
{
    public interface IWx3rdService
    {
        Task<LoginResponse> Login(string host, LoginRequest loginRequest);
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

            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            return loginResponse;
        }
    }
}
