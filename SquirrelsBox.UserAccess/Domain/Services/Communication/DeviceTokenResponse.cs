using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.UserAccess.Domain.Models;

namespace SquirrelsBox.UserAccess.Domain.Services.Communication
{
    public class DeviceTokenResponse : BaseResponse<DeviceToken>
    {
        public DeviceTokenResponse(string message) : base(message)
        {
        }

        public DeviceTokenResponse(DeviceToken resource) : base(resource)
        {
        }
    }
}
