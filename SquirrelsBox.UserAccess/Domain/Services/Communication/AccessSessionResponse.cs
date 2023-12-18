using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Session.Domain.Models;

namespace SquirrelsBox.Session.Domain.Services.Communication
{
    public class AccessSessionResponse : BaseResponse<Models.AccessSession>
    {
        public AccessSessionResponse(string message) : base(message)
        {
        }

        public AccessSessionResponse(Models.AccessSession resource) : base(resource)
        {
        }
    }
}
