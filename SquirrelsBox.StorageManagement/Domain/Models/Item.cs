using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class Item : DateAuditory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCodeOwner { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string? ItemPhoto { get; set; }
        public bool State { get; set; }

        public ICollection<SectionItemsList> SectionItemsList { get; set; }
        public ICollection<ItemSpecsList> ItemSpecsList { get; set; }
    }
}
