using AutoMapper;
using SquirrelsBox.Permissions.Domain.Models;
using SquirrelsBox.Permissions.Resources;

namespace SquirrelsBox.Permissions.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAssignedPermissionResource, AssignedPermission>();
        }
    }
}
