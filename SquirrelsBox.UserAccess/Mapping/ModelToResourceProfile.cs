using AutoMapper;
using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Generic.Resources;

namespace SquirrelsBox.Session.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {

            //Validation Resource
            CreateMap(typeof(BaseResponse<>), typeof(ValidationResource));
        }
    }
}
