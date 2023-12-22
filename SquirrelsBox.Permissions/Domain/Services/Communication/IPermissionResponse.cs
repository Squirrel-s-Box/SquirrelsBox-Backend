using SquirrelsBox.Permissions.Domain.Models;

namespace SquirrelsBox.Permissions.Domain.Services.Communication
{
    public interface IPermissionResponse
    {
        bool Success { get; }
        Permission PermissionResource { get; }
    }
}
