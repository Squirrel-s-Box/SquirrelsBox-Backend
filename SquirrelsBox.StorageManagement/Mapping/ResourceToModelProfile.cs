using AutoMapper;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Resources;

namespace SquirrelsBox.StorageManagement.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<ReadBoxResource, Box>();
            CreateMap<SaveBoxResource, Box>();
            CreateMap<UpdateBoxResource, Box>();

            CreateMap<SaveBoxSectionsListResource, BoxSectionRelationship>();
            CreateMap<SaveSectionResource, Section>();
            CreateMap<UpdateBoxSectionsListResource, BoxSectionRelationship>();
            CreateMap<UpdateSectionResource, Section>();
        }
    }
}
