using choapi.Models;

namespace choapi.Messages
{
    public class NonOperatingHourResponse : ResponseBase
    {
        public NonOperatingHours NonOperatingHours { get; set; } = new NonOperatingHours();
    }
}
