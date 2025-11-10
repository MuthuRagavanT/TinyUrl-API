namespace TinyUrl_Api.ViewModels
{
    public class InsertViewModel
    {
        public string originalUrl { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
    }
}
