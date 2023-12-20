using Newtonsoft.Json;
using RestSharp;
using System;
using Wx3rdApi.Models.Wx;
using static System.Collections.Specialized.BitVector32;

namespace Wx3rdApi.Services
{
    public interface IWxService
    {
        Task<ListDraftResponse> ListDraft(string componentAccessToken);

        Task<AddTemplateResponse> AddTemplate(string componentAccessToken, int draftId, int templateType);

        Task<ListTemplateResponse> ListTemplate(string componentAccessToken, int templateType);

        Task<DeleteTemplateResponse> DeleteTemplate(string componentAccessToken, int templateId);

        Task<ModifyThirdpartyServerDomainResponse> ModifyThirdpartyServerDomain(string componentAccessToken, string action, bool isModifyPublishedTogether, string wxaServerDomain);

        Task<ModifyThirdpartyJumpDomainResponse> ModifyThirdpartyJumpDomain(string componentAccessToken, string action, bool isModifyPublishedTogether, string wxaJumpH5Domain);

        Task<GetThirdpartyDomainConfirmFileResponse> GetThirdpartyDomainConfirmFile(string componentAccessToken);

        Task<GetEffectiveServerDomainResponse> GetAppEffectiveServerDomain(string authorizerAccessToken);

        Task<GetBasicInfoResponse> GetAccountBasicInfo(string authorizerAccessToken);

        Task<RegisterMiniProgramResponse> RegisterMiniProgram(string componentAccessToken, RegisterMiniProgramRequest registerMiniProgramRequest);
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

        public async Task<ModifyThirdpartyJumpDomainResponse> ModifyThirdpartyJumpDomain(string componentAccessToken, string action, bool isModifyPublishedTogether, string wxaJumpH5Domain)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/modify_wxa_jump_domain?access_token={componentAccessToken}";
            var request = new RestRequest(url);

            var modifyThirdpartyJumpDomainRequest = new ModifyThirdpartyJumpDomainRequest
            {
                action = action,
                is_modify_published_together = isModifyPublishedTogether,
                wxa_jump_h5_domain = wxaJumpH5Domain
            };

            var body = JsonConvert.SerializeObject(modifyThirdpartyJumpDomainRequest);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var modifyThirdpartyJumpDomainResponse = JsonConvert.DeserializeObject<ModifyThirdpartyJumpDomainResponse>(response.Content);
            return modifyThirdpartyJumpDomainResponse;
        }

        public async Task<GetThirdpartyDomainConfirmFileResponse> GetThirdpartyDomainConfirmFile(string componentAccessToken)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/get_domain_confirmfile?access_token={componentAccessToken}";
            var request = new RestRequest(url);

            request.AddParameter("application/json", "{}", ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var getThirdpartyDomainConfirmFileResponse = JsonConvert.DeserializeObject<GetThirdpartyDomainConfirmFileResponse>(response.Content);
            return getThirdpartyDomainConfirmFileResponse;
        }

        public async Task<GetEffectiveServerDomainResponse> GetAppEffectiveServerDomain(string authorizerAccessToken)
        {
            var url = $"https://api.weixin.qq.com/wxa/get_effective_domain?access_token={authorizerAccessToken}";
            var request = new RestRequest(url);

            request.AddParameter("application/json", "{}", ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var getEffectiveServerDomainResponse = JsonConvert.DeserializeObject<GetEffectiveServerDomainResponse>(response.Content);
            return getEffectiveServerDomainResponse;
        }

        public async Task<GetBasicInfoResponse> GetAccountBasicInfo(string authorizerAccessToken)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/account/getaccountbasicinfo?access_token={authorizerAccessToken}";
            var request = new RestRequest(url);

            request.AddParameter("application/json", "{}", ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var getBasicInfoResponse = JsonConvert.DeserializeObject<GetBasicInfoResponse>(response.Content);
            return getBasicInfoResponse;
        }

        public async Task<RegisterMiniProgramResponse> RegisterMiniProgram(string componentAccessToken, RegisterMiniProgramRequest registerMiniProgramRequest)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/fastregisterweapp?action=create&component_access_token={componentAccessToken}";
            var request = new RestRequest(url);

            var body = JsonConvert.SerializeObject(registerMiniProgramRequest);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            using var restClient = new RestClient(_httpClientFactory.CreateClient(), new RestClientOptions { MaxTimeout = _maxTimeout });
            var response = await restClient.ExecuteAsync(request, Method.Post);

            var registerMiniProgramResponse = JsonConvert.DeserializeObject<RegisterMiniProgramResponse>(response.Content);
            return registerMiniProgramResponse;
        }
    }
}
