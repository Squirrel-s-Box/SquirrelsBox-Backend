using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class ItemResponse : BaseResponse<Item>
    {
        public ItemResponse(string message) : base(message)
        {
        }

        public ItemResponse(Item resource) : base(resource)
        {
        }
    }
}
