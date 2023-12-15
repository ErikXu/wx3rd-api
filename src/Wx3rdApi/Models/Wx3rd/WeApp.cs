using System.ComponentModel.DataAnnotations;

namespace Wx3rdApi.Models.Wx3rd
{
    public class ListWeAppForm
    {
        [Required]
        public int Offset { get; set; }

        [Required]
        public int Limit { get; set; }
    }

    public class ListWeAppResponse : BaseResponse
    {
        public ListWeAppData data { get; set; }
    }

    public class ListWeAppData
    {
        public List<ListWeAppRecord> records { get; set; }

        public int total { get; set; }
    }

    public class ListWeAppRecord
    {
        public string appid { get; set; }

        public string nickName { get; set; }

        public List<int> funcInfo { get; set; }

        public string qrCodeUrl { get; set; }

        public int serviceStatus { get; set; }

        public ReleaseInfo releaseInfo { get; set; }

        public ExpInfo expInfo { get; set; }
    }

    public class ReleaseInfo
    {
        public int releaseTime { get; set; }

        public string releaseVersion { get; set; }

        public string releaseDesc { get; set; }
    }

    public class ExpInfo
    {
        public int expTime { get; set; }

        public string expVersion { get; set; }

        public string expDesc { get; set; }
    }
}
