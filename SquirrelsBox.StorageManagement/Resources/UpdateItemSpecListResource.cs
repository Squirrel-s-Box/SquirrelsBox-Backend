namespace SquirrelsBox.StorageManagement.Resources
{
    public class UpdateItemSpecListResource
    {
        public int? ItemId { get; set; }
        public UpdateSpecResource Spec { get; set; }
    }

    public class UpdateSpecResource
    {
        public string HeaderName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }
}
