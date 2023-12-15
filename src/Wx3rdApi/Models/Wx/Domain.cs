using System.ComponentModel.DataAnnotations;

namespace Wx3rdApi.Models.Wx
{
    public class ModifyThirdpartyServerDomainRequest
    {
        public string action { get; set; }

        public bool is_modify_published_together { get; set; }

        public string wxa_server_domain { get; set; }
    }

    public class ModifyThirdpartyServerDomainResponse : BaseResponse
    {
        public string published_wxa_server_domain { get; set; }
        public string testing_wxa_server_domain { get; set; }
    }

    public class ModifyThirdpartyJumpDomainRequest
    {
        public string action { get; set; }

        public bool is_modify_published_together { get; set; }

        public string wxa_jump_h5_domain { get; set; }
    }

    public class ModifyThirdpartyJumpDomainResponse : BaseResponse
    {
        public string published_wxa_jump_h5_domain { get; set; }
        public string testing_wxa_jump_h5_domain { get; set; }
    }

    public class GetAppDomainForm
    {
        /// <summary>
        /// 小程序 Id
        /// </summary>
        [Required]
        public string AppId { get; set; }
    }


    public class GetEffectiveServerDomainResponse : BaseResponse
    {
        public MpDomain mp_domain { get; set; }

        public ThirdDomain third_domain { get; set; }

        public DirectDomain direct_domain { get; set; }

        public EffectiveDomain effective_domain { get; set; }
    }

    public class MpDomain
    {
        public List<string> requestdomain { get; set; }

        public List<string> wsrequestdomain { get; set; }

        public List<string> uploaddomain { get; set; }

        public List<string> downloaddomain { get; set; }

        public List<string> udpdomain { get; set; }

        public List<string> tcpdomain { get; set; }
    }

    public class ThirdDomain
    {
        public List<string> requestdomain { get; set; }

        public List<string> wsrequestdomain { get; set; }

        public List<string> uploaddomain { get; set; }

        public List<string> downloaddomain { get; set; }

        public List<string> udpdomain { get; set; }

        public List<string> tcpdomain { get; set; }
    }

    public class DirectDomain
    {
        public List<string> requestdomain { get; set; }

        public List<string> wsrequestdomain { get; set; }

        public List<string> uploaddomain { get; set; }

        public List<string> downloaddomain { get; set; }

        public List<string> udpdomain { get; set; }

        public List<string> tcpdomain { get; set; }
    }

    public class EffectiveDomain
    {
        public List<string> requestdomain { get; set; }

        public List<string> wsrequestdomain { get; set; }

        public List<string> uploaddomain { get; set; }

        public List<string> downloaddomain { get; set; }

        public List<string> udpdomain { get; set; }

        public List<string> tcpdomain { get; set; }
    }
}
