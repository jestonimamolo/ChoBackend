using choapi.Models;

namespace choapi.Messages
{
    public class AppInfoResponse : ResponseBase
    {
        public AppInfo AppInfo { get; set; } = new AppInfo();
    }
}
