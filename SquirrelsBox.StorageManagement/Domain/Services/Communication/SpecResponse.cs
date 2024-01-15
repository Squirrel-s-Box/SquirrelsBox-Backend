using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class SpecResponse : BaseResponse<PersonalizedSpec>
    {
        public SpecResponse(string message) : base(message)
        {
        }

        public SpecResponse(PersonalizedSpec resource) : base(resource)
        {
        }
    }
}
