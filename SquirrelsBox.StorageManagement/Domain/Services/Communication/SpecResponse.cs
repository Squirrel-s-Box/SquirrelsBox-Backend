using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class SpecResponse : BaseResponse<Spec>
    {
        public SpecResponse(string message) : base(message)
        {
        }

        public SpecResponse(Spec resource) : base(resource)
        {
        }
    }
}
