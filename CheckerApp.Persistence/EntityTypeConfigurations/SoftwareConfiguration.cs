using CheckerApp.Domain.Entities.SoftwareEntities;
using CheckerApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
    {
        public void Configure(EntityTypeBuilder<Software> builder)
        {
            builder.HasDiscriminator(e => e.SoftwareType)
                .HasValue<SCADA>(SoftwareType.SCADA)
                .HasValue<Software>(SoftwareType.Other);
        }
    }
}
