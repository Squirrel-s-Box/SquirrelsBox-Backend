namespace SquirrelsBox.StorageManagement.Resources
{
    public class SaveBoxSectionsListResource
    {
        public int BoxId { get; set; }
        public SaveSectionResource Section { get; set; }
    }

    public class SaveSectionResource
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
