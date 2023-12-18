using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.Session.Domain.Models
{
    public class AccessSession : DateAuditory
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Attempt { get; set; }

        public IList<UserSession> SessionsTokens { get; set; }
        public IList<DeviceSession> DevicesTokens { get; set; }
    }
}
