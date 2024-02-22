namespace SquirrelsBox.StorageManagement.Resources
{
    public class SaveSectionItemResource
    {
        public int SectionId { get; set; }
        public SaveItemResource Item { get; set; }
    }
    public class SaveItemResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string ItemPhoto { get; set; }
    }
}
