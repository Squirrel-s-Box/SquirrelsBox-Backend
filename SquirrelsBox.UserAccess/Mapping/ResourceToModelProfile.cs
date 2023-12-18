using AutoMapper;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Resources;

namespace SquirrelsBox.Session.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveTokenResource, SaveFoundTokenByUserIdResource>();
            CreateMap<SaveFoundTokenByUserIdResource, DeviceSession>();
            CreateMap<SaveFoundTokenByUserIdResource, UserSession>();
            CreateMap<SaveTokenResource, DeviceSession>();

            CreateMap<UpdateUserResource, AccessSession>();
        }
    }
}
