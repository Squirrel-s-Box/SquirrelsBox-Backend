using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class Section : DateAuditory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool State { get; set; }

        public ICollection<BoxSectionRelationship> BoxSectionsList { get; set; }
        public ICollection<SectionItemRelationship> SectionItemsList { get; set; }
    }
}
