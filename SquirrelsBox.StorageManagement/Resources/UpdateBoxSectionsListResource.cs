namespace SquirrelsBox.StorageManagement.Resources
{
    public class UpdateBoxSectionsListResource
    {
        public int? BoxId { get; set; }
        public SaveSectionResource Section { get; set; }
    }

    public class UpdateSectionResource
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool State { get; set; }
    }
}
