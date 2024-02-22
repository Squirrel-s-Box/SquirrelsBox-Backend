using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Resources
{
    public class ReadItemSpecRelationshipResource
    {
        public ReadSpecResource Item { get; set; }
    }

    public class ReadSpecResource : DateAuditory
    {
        public int Id { get; set; }
        public string HeaderName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }
}
