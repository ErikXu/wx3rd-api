namespace Wx3rdApi.Models.Wx
{
    public class ListDraftResponse: BaseResponse
    {
        public List<Draft> draft_list { get; set; }
    }

    public class Draft_List
    {
        public int create_time { get; set; }
        public string user_version { get; set; }
        public string user_desc { get; set; }
        public int draft_id { get; set; }
    }

    public class Draft
    {
        public int create_time { get; set; }

        public string user_version { get; set; }

        public string user_desc { get; set; }

        public int draft_id { get; set; }

        public string source_miniprogram_appid { get; set; }

        public string source_miniprogram { get; set; }

        public string developer { get; set; }

        public object[] category_list { get; set; }
    }
}
