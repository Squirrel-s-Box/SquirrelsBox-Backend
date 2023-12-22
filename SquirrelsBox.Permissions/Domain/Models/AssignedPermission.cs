using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.Permissions.Domain.Models
{
    public class AssignedPermission : DateAuditory
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public int ElementId { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
