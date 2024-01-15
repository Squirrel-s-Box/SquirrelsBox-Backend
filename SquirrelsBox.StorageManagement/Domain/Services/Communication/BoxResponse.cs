using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class BoxResponse : BaseResponse<Box>
    {
        public BoxResponse(string message) : base(message)
        {
        }

        public BoxResponse(Box resource) : base(resource)
        {
        }
    }
}
