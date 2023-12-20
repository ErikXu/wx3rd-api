using System.ComponentModel.DataAnnotations;

namespace Wx3rdApi.Models.Wx
{
    public class GetBasicInfoForm
    {
        /// <summary>
        /// 小程序 Id
        /// </summary>
        [Required]
        public string AppId { get; set; }
    }

    public class GetBasicInfoResponse : BaseResponse
    {
        public string appid { get; set; }
        public int account_type { get; set; }
        public int principal_type { get; set; }
        public string principal_name { get; set; }
        public int realname_status { get; set; }
        public Wx_Verify_Info wx_verify_info { get; set; }
        public Signature_Info signature_info { get; set; }
        public Head_Image_Info head_image_info { get; set; }
        public string nickname { get; set; }
        public int registered_country { get; set; }
        public Nickname_Info nickname_info { get; set; }
        public string credential { get; set; }
        public int customer_type { get; set; }
    }

    public class Wx_Verify_Info
    {
        public bool qualification_verify { get; set; }
        public bool naming_verify { get; set; }
        public bool annual_review { get; set; }
        public int annual_review_begin_time { get; set; }
        public int annual_review_end_time { get; set; }
    }

    public class Signature_Info
    {
        public string signature { get; set; }
        public int modify_used_count { get; set; }
        public int modify_quota { get; set; }
    }

    public class Head_Image_Info
    {
        public string head_image_url { get; set; }
        public int modify_used_count { get; set; }
        public int modify_quota { get; set; }
    }

    public class Nickname_Info
    {
        public string nickname { get; set; }
        public int modify_used_count { get; set; }
        public int modify_quota { get; set; }
    }
}
