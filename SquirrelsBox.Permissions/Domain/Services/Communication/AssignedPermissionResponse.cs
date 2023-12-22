using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Permissions.Domain.Models;

namespace SquirrelsBox.Permissions.Domain.Services.Communication
{
    public class AssignedPermissionResponse : BaseResponse<AssignedPermission>, IPermissionResponse
    {
        public AssignedPermissionResponse(string message) : base(message)
        {
        }

        public AssignedPermissionResponse(AssignedPermission resource) : base(resource)
        {
        }

        public Permission PermissionResource => Resource?.Permission;
    }
}
