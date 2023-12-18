using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Session.Domain.Models;

namespace SquirrelsBox.Session.Domain.Services.Communication
{
    public class UserSessionResponse : BaseResponse<UserSession>
    {
        public UserSessionResponse(string message) : base(message)
        {
        }

        public UserSessionResponse(UserSession resource) : base(resource)
        {
        }
    }
}
