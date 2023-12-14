using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.UserAccess.Domain.Models
{
    public class DeviceToken : DateAuditory
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }

    }
}
