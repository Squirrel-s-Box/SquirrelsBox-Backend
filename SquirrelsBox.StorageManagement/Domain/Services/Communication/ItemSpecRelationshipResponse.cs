using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class ItemSpecRelationshipResponse : BaseResponse<ItemSpecRelationship>
    {
        public ItemSpecRelationshipResponse(string message) : base(message)
        {
        }

        public ItemSpecRelationshipResponse(ItemSpecRelationship resource) : base(resource)
        {
        }
    }
}
