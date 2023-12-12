using Newtonsoft.Json;
using RestSharp;
using Wx3rdApi.Models.Wx;

namespace Wx3rdApi.Services
{
    public interface IWxService
    {
        Task<ListDraftResponse> ListDraft(string componentAccessToken);

        Task<ListTemplateResponse> ListTemplate(string componentAccessToken, int templateType);
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

        public async Task<ListTemplateResponse> ListTemplate(string componentAccessToken, int templateType)
        {
            var url = $"https://api.weixin.qq.com/wxa/gettemplatelist?access_token={componentAccessToken}&template_type={templateType}";
            var request = new RestRequest(url);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.GetAsync(request);

            var listTemplateResponse = JsonConvert.DeserializeObject<ListTemplateResponse>(response.Content);
            return listTemplateResponse;
        }
    }
}
