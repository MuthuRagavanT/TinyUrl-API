namespace TinyUrl_Api.Models
{
    public class TinyUrlModel
    {

        public string LongUrl { get; set; } = string.Empty;

        public string ShortUrl { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        public bool IsPrivate { get; set; }

        public int ClickCount { get; set; }
    }
}
