using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Resources
{
    public class ReadBoxSectionRelationshipResource
    {
        public ReadSectionResource Section { get; set; }
    }

    public class ReadSectionResource : DateAuditory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
