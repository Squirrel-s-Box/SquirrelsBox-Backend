namespace SquirrelsBox.StorageManagement.Resources
{
    public class SaveItemSpecListResource
    {
        public int ItemId { get; set; }
        public SaveSpecResource Spec { get; set; }
    }
    public class SaveSpecResource
    {
        public string HeaderName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }
}
