using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.UserAccess.Domain.Models;

namespace SquirrelsBox.UserAccess.Domain.Services.Communication
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(string message) : base(message)
        {
        }

        public UserResponse(User resource) : base(resource)
        {
        }
    }
}
