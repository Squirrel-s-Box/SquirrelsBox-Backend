using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.UserAccess.Domain.Models;

namespace SquirrelsBox.UserAccess.Domain.Services.Communication
{
    public class AccessTokenResponse : BaseResponse<AccessToken>
    {
        public AccessTokenResponse(string message) : base(message)
        {
        }

        public AccessTokenResponse(AccessToken resource) : base(resource)
        {
        }
    }
}
