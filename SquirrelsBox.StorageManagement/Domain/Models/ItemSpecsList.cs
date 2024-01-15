namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class ItemSpecsList
    {
        public int ItemId { get; set; }
        public int SpecsId { get; set; }

        public Item Item { get; set; }
        public PersonalizedSpec Spec { get; set; }
    }
}
