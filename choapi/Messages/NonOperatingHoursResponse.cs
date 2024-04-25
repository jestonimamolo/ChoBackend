using choapi.Models;

namespace choapi.Messages
{
    public class NonOperatingHoursResponse : ResponseBase
    {
        public List<NonOperatingHours> NonOperatingHours { get; set; } = new List<NonOperatingHours>();
    }
}
