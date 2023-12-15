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

}
