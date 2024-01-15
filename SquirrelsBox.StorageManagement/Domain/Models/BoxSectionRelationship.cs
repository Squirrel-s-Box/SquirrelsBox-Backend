namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class BoxSectionRelationship
    {
        public int BoxId { get; set; }
        public int SectionId { get; set; }

        public Box? Box { get; set; }
        public Section Section { get; set; }
    }
}
