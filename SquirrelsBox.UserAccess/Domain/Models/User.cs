using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.UserAccess.Domain.Models
{
    public class User : DateAuditory
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
        public string Attempt { get; set; }
    }
}
