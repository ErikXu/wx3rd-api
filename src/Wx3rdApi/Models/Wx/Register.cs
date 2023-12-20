using System.ComponentModel.DataAnnotations;

namespace Wx3rdApi.Models.Wx
{
    public class RegisterMiniProgramForm
    {
        /// <summary>
        /// 企业名（需与工商部门登记信息一致）；如果是“无主体名称个体工商户”则填“个体户+法人姓名”，例如“个体户张三”
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 企业代码
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 企业代码类型 1：统一社会信用代码（18 位） 2：组织机构代码（9 位 xxxxxxxx-x） 3：营业执照注册号(15 位)
        /// </summary>
        [Required]
        public int CodeType { get; set; }

        /// <summary>
        /// 法人微信号
        /// </summary>
        [Required]
        public string LegalPersonaWechat { get; set; }

        /// <summary>
        /// 法人姓名（绑定银行卡）
        /// </summary>
        [Required]
        public string LegalPersonaName { get; set; }

        /// <summary>
        /// 第三方联系电话
        /// </summary>
        public string ComponentPhone { get; set; }
    }

    public class RegisterMiniProgramRequest
    {
        public string name { get; set; }

        public string code { get; set; }

        public int code_type { get; set; }

        public string legal_persona_wechat { get; set; }

        public string legal_persona_name { get; set; }

        public string component_phone { get; set; }
    }

    public class RegisterMiniProgramResponse : BaseResponse
    {

    }

    public class SearchRegisterMiniProgramForm
    {
        /// <summary>
        /// 企业名（需与工商部门登记信息一致）；如果是“无主体名称个体工商户”则填“个体户+法人姓名”，例如“个体户张三”
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 法人微信号
        /// </summary>
        [Required]
        public string LegalPersonaWechat { get; set; }

        /// <summary>
        /// 法人姓名（绑定银行卡）
        /// </summary>
        [Required]
        public string LegalPersonaName { get; set; }
    }

    public class SearchRegisterMiniProgramRequest
    {
        public string name { get; set; }

        public string legal_persona_wechat { get; set; }

        public string legal_persona_name { get; set; }
    }

    public class SearchRegisterMiniProgramResponse : BaseResponse
    {

    }

    public class RegisterPersonalMiniProgramForm
    {
        /// <summary>
        /// 个人用户名字
        /// </summary>
        [Required]
        public string IdName { get; set; }

        /// <summary>
        /// 个人用户微信号
        /// </summary>
        [Required]
        public string WxUser { get; set; }

        /// <summary>
        /// 第三方联系电话
        /// </summary>
        public string ComponentPhone { get; set; }
    }

    public class RegisterPersonalMiniProgramRequest
    {
        public string idname { get; set; }

        public string wxuser { get; set; }

        public string component_phone { get; set; }
    }

    public class RegisterPersonalMiniProgramResponse : BaseResponse
    {
        public string taskid { get; set; }

        public string authorize_url { get; set; }

        public int status { get; set; }
    }

    public class SearchRegisterPersonalMiniProgramForm
    {
        /// <summary>
        /// 任务 Id
        /// </summary>
        [Required]
        public string TaskId { get; set; }
    }

    public class SearchRegisterPersonalMiniProgramRequest
    {
        public string taskid { get; set; }
    }

    public class SearchRegisterPersonalMiniProgramResponse : BaseResponse
    {

    }
}
