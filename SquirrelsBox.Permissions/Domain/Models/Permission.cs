using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.Permissions.Domain.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Collection { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
