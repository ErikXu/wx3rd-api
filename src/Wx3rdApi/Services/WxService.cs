using Newtonsoft.Json;
using RestSharp;
using Wx3rdApi.Models.Wx;

namespace Wx3rdApi.Services
{
    public interface IWxService
    {
        Task<ListDraftResponse> ListDraft(string componentAccessToken);

        Task<AddTemplateResponse> AddTemplate(string componentAccessToken, int draftId, int templateType);

        Task<ListTemplateResponse> ListTemplate(string componentAccessToken, int templateType);

        Task<DeleteTemplateResponse> DeleteTemplate(string componentAccessToken, int templateId);

        Task<ModifyThirdpartyServerDomainResponse> ModifyThirdpartyServerDomain(string componentAccessToken, string action, bool isModifyPublishedTogether, string wxaServerDomain);
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

        public async Task<AddTemplateResponse> AddTemplate(string componentAccessToken, int draftId, int templateType)
        {
            var url = $"https://api.weixin.qq.com/wxa/addtotemplate?access_token={componentAccessToken}";
            var request = new RestRequest(url);

            var addTemplateRequest = new AddTemplateRequest
            {
                draft_id = draftId,
                template_type = templateType
            };

            var body = JsonConvert.SerializeObject(addTemplateRequest);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var addTemplateResponse = JsonConvert.DeserializeObject<AddTemplateResponse>(response.Content);
            return addTemplateResponse;
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

        public async Task<DeleteTemplateResponse> DeleteTemplate(string componentAccessToken, int templateId)
        {
            var url = $"https://api.weixin.qq.com/wxa/deletetemplate?access_token={componentAccessToken}";
            var request = new RestRequest(url);

            var deleteTemplateRequest = new DeleteTemplateRequest
            {
                template_id = templateId
            };

            var body = JsonConvert.SerializeObject(deleteTemplateRequest);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var deleteTemplateResponse = JsonConvert.DeserializeObject<DeleteTemplateResponse>(response.Content);
            return deleteTemplateResponse;
        }

        public async Task<ModifyThirdpartyServerDomainResponse> ModifyThirdpartyServerDomain(string componentAccessToken, string action, bool isModifyPublishedTogether, string wxaServerDomain)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/modify_wxa_server_domain?access_token={componentAccessToken}";
            var request = new RestRequest(url);

            var modifyThirdpartyServerDomainRequest = new ModifyThirdpartyServerDomainRequest
            {
               action = action,
               is_modify_published_together = isModifyPublishedTogether,
               wxa_server_domain = wxaServerDomain
            };

            var body = JsonConvert.SerializeObject(modifyThirdpartyServerDomainRequest);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var modifyThirdpartyServerDomainResponse = JsonConvert.DeserializeObject<ModifyThirdpartyServerDomainResponse>(response.Content);
            return modifyThirdpartyServerDomainResponse;
        }
    }
}
