using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Session.Domain.Models;

namespace SquirrelsBox.Session.Domain.Services.Communication
{
    public class DeviceSessionResponse : BaseResponse<DeviceSession>
    {
        public DeviceSessionResponse(string message) : base(message)
        {
        }

        public DeviceSessionResponse(DeviceSession resource) : base(resource)
        {
        }
    }
}
