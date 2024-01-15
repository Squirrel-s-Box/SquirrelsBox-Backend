using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class SharedBox : DateAuditory
    {
        public int Id { get; set; }
        public int userCodeGuest { get; set; }
        public bool State { get; set; }

        public int BoxId { get; set; }
        public Box Box { get; set; }
    }
}
