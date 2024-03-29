﻿using AutoMapper;
using SquirrelsBox.Generic.Domain.Services.Communication;
using SquirrelsBox.Generic.Resources;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Resources;

namespace SquirrelsBox.StorageManagement.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Box, ReadBoxResource>();

            CreateMap<BoxSectionRelationship, ReadBoxSectionRelationshipResource>();
            CreateMap<Section, ReadSectionResource>();

            CreateMap<SectionItemRelationship, ReadSectionItemRelationshipResource>();
            CreateMap<Item, ReadItemResource>();

            CreateMap<ItemSpecRelationship, ReadItemSpecRelationshipResource>();
            CreateMap<Spec, ReadSpecResource>();


            //Validation Resource
            CreateMap(typeof(BaseResponse<>), typeof(ValidationResource));
        }
    }
}
