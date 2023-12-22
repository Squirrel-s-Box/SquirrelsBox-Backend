using AutoMapper;
using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Generic.Resources;
using SquirrelsBox.Permissions.Domain.Services.Communication;
using SquirrelsBox.Permissions.Resources;
using System.Collections.Generic;

namespace SquirrelsBox.Permissions.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<AssignedPermissionResponse, ListAssignedPermission>();
            //Validation Resource
            CreateMap(typeof(BaseResponse<>), typeof(ValidationResource));
        }
    }
}
