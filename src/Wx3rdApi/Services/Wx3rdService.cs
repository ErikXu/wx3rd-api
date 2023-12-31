﻿using Newtonsoft.Json;
using RestSharp;
using Wx3rdApi.Models.Wx3rd;

namespace Wx3rdApi.Services
{
    public interface IWx3rdService
    {
        Task<LoginResponse> Login(string host, LoginRequest loginRequest);

        Task<GetComponentAccessTokenResponse> GetComponentAccessToken(string host, string jwt);

        Task<ListWeAppResponse> ListWeApp(string host, string jwt, int offset, int limit);

        Task<GetAuthorizerAccessTokenResponse> GetAuthorizerAccessToken(string host, string jwt, string appId);
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
            if (baseResponse.code == 0)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
                return loginResponse;
            }
            else
            {
                return new LoginResponse
                {
                    code = baseResponse.code,
                    errorMsg = baseResponse.errorMsg
                };
            }
        }

        public async Task<GetComponentAccessTokenResponse> GetComponentAccessToken(string host, string jwt)
        {
            var url = $"{host}/wxcomponent/admin/component-access-token";
            var request = new RestRequest(url);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));

            using var restClient = new RestClient(new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.GetAsync(request);

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
            if (baseResponse.code == 0)
            {
                var getComponentAccessTokenResponse = JsonConvert.DeserializeObject<GetComponentAccessTokenResponse>(response.Content);
                return getComponentAccessTokenResponse;
            }
            else
            {
                return new GetComponentAccessTokenResponse
                {
                    code = baseResponse.code,
                    errorMsg = baseResponse.errorMsg
                };
            }
        }

        public async Task<ListWeAppResponse> ListWeApp(string host, string jwt, int offset, int limit)
        {
            var url = $"{host}/wxcomponent/admin/dev-weapp-list?offset={offset}&limit={limit}";
            var request = new RestRequest(url);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));

            using var restClient = new RestClient(new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.GetAsync(request);

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
            if (baseResponse.code == 0)
            {
                var listWeAppResponse = JsonConvert.DeserializeObject<ListWeAppResponse>(response.Content);
                return listWeAppResponse;
            }
            else
            {
                return new ListWeAppResponse
                {
                    code = baseResponse.code,
                    errorMsg = baseResponse.errorMsg
                };
            }
        }

        public async Task<GetAuthorizerAccessTokenResponse> GetAuthorizerAccessToken(string host, string jwt, string appId)
        {
            var url = $"{host}/wxcomponent/admin/authorizer-access-token?appid={appId}";
            var request = new RestRequest(url);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));

            using var restClient = new RestClient(new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.GetAsync(request);

            var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(response.Content);
            if (baseResponse.code == 0)
            {
                var getAuthorizerAccessTokenResponse = JsonConvert.DeserializeObject<GetAuthorizerAccessTokenResponse>(response.Content);
                return getAuthorizerAccessTokenResponse;
            }
            else
            {
                return new GetAuthorizerAccessTokenResponse
                {
                    code = baseResponse.code,
                    errorMsg = baseResponse.errorMsg
                };
            }
        }
    }
}
