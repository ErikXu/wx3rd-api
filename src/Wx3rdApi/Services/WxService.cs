using Newtonsoft.Json;
using RestSharp;
using Wx3rdApi.Models.Wx;

namespace Wx3rdApi.Services
{
    public interface IWxService
    {
        Task<ListDraftResponse> ListDraft(string componentAccessToken);
    }

    public class WxService: IWxService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly int _maxTimeout = 30000;

        public WxService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ListDraftResponse> ListDraft(string componentAccessToken)
        {
            var url = $"https://api.weixin.qq.com/wxa/gettemplatedraftlist?access_token={componentAccessToken}";
            var request = new RestRequest(url);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.GetAsync(request);

            var listDraftResponse = JsonConvert.DeserializeObject<ListDraftResponse>(response.Content);
            return listDraftResponse;
        }
    }
}
