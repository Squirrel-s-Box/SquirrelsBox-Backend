using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Resources
{
    public class ReadSectionItemRelationshipResource
    {
        public ReadItemResource Item { get; set; }
    }
    public class ReadItemResource : DateAuditory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string ItemPhoto { get; set; }
    }
}
