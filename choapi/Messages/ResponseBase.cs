namespace choapi.Messages
{
    public class ResponseBase
    {
        public string Status { get; set; } = "Success";
        public string? Message { get; set; }
    }
}
