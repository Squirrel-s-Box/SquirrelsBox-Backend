using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class SharedBoxResponse : BaseResponse<SharedBox>
    {
        public SharedBoxResponse(string message) : base(message)
        {
        }

        public SharedBoxResponse(SharedBox resource) : base(resource)
        {
        }
    }
}
