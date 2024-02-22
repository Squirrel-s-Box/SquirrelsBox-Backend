using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Services.Communication
{
    public class SectionItemRelationshipResponse : BaseResponse<SectionItemRelationship>
    {
        public SectionItemRelationshipResponse(string message) : base(message)
        {
        }

        public SectionItemRelationshipResponse(SectionItemRelationship resource) : base(resource)
        {
        }
    }
}
