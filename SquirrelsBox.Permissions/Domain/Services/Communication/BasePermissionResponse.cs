using SquirrelsBox.Permissions.Domain.Models;

namespace SquirrelsBox.Permissions.Domain.Services.Communication
{
        public class BasePermissionResponse
        {
            public bool Success { get; protected set; }
            public Permission PermissionResource { get; protected set; }

            public BasePermissionResponse(Permission resource)
            {
                if (resource == null)
                {
                    Success = false;
                    PermissionResource = resource;
                }
                else
                {
                    Success = true;
                    PermissionResource = resource;
                }
            }
        }
}
