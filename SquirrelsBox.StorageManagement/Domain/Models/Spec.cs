using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class Spec : DateAuditory
    {
        public int Id { get; set; }
        public string HeaderName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public bool State { get; set; }

        public ICollection<ItemSpecRelationship> ItemSpecsList { get; set; }
    }
}
