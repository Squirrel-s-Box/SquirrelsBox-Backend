namespace SquirrelsBox.StorageManagement.Resources
{
    public class UpdateSectionItemListResource
    {
        public int? SectionId { get; set; }
        public UpdateItemResource Item { get; set; }
    }

    public class UpdateItemResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string ItemPhoto { get; set; }
    }
}
