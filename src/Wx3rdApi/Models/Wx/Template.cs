using System.ComponentModel.DataAnnotations;

namespace Wx3rdApi.Models.Wx
{
    public class ListTemplateForm
    {
        /// <summary>
        /// 模板类型，0 - 对应普通模板；1 - 对应标准模板库
        /// </summary>
        [Required]
        public int TemplateType { get; set; }
    }

    public class ListTemplateResponse : BaseResponse
    {
        public List<TemplateListItem> template_list { get; set; }
    }

    public class TemplateListItem
    {
        public int create_time { get; set; }

        public string user_version { get; set; }

        public string user_desc { get; set; }

        public int template_id { get; set; }

        public int draft_id { get; set; }

        public string source_miniprogram_appid { get; set; }

        public string source_miniprogram { get; set; }

        public string developer { get; set; }

        public int template_type { get; set; }

        public object[] category_list { get; set; }
    }
}
