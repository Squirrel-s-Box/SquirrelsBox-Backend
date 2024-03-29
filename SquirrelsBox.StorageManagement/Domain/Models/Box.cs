﻿using SquirrelsBox.Generic.Domain.Models;

namespace SquirrelsBox.StorageManagement.Domain.Models
{
    public class Box : DateAuditory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCodeOwner { get; set; }
        public bool Favourite { get; set; }
        public bool State { get; set; }

        public ICollection<BoxSectionRelationship>  BoxSectionsList { get; set; }
        public ICollection<SharedBox> SharedBoxes { get; set; }
    }
}
