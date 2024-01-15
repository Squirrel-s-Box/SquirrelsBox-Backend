namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class SectionItemsList
    {
        public int SectionId { get; set; }
        public int ItemId { get; set; }

        public Section Section { get; set; }
        public Item Item { get; set; }
    }
}
