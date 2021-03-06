using AutoMapper;
using CheckerApp.Application.Common.Mappings;
using CheckerApp.Domain.Entities.SoftwareEntities;

namespace CheckerApp.Application.Softwares.Queries.GetSoftware
{
    public class SoftwareDto : IMapFrom<Software>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Software, SoftwareDto>().IncludeAllDerived();
        }
    }
}
