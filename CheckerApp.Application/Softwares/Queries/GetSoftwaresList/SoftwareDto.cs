﻿using AutoMapper;
using CheckerApp.Application.Common.Mappings;
using CheckerApp.Domain.Entities.SoftwareEntities;
using CheckerApp.Domain.Enums;

namespace CheckerApp.Application.Softwares.Queries.GetSoftwaresList
{
    public class SoftwareDto : IMapFrom<Software>
    {
        public SoftwareDto()
        {
            SoftwareType = SoftwareType.Other;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public SoftwareType SoftwareType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Software, SoftwareDto>().IncludeAllDerived();
        }
    }
}
