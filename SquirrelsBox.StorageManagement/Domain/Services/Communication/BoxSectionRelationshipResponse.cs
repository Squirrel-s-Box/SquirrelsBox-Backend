using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class BoxSectionRelationshipResponse : BaseResponse<BoxSectionRelationship>
    {
        public BoxSectionRelationshipResponse(string message) : base(message)
        {
        }

        public BoxSectionRelationshipResponse(BoxSectionRelationship resource) : base(resource)
        {
        }
    }
}
