using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.UserAccess.Domain.Models
{
    public class AccessToken : DateAuditory
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string OldToken { get; set; }
        public string DeviceToken { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
