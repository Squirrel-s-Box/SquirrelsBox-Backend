using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Permissions.Domain.Models;

namespace SquirrelsBox.Permissions.Domain.Services.Communication
{
    public class PermissionResponse : BaseResponse<Permission>
    {
        public PermissionResponse(string message) : base(message)
        {
        }

        public PermissionResponse(Permission resource) : base(resource)
        {
        }
    }
}
