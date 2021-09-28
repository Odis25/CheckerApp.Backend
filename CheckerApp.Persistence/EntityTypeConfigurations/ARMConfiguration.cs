using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class ARMConfiguration : IEntityTypeConfiguration<ARM>
    {
        public void Configure(EntityTypeBuilder<ARM> builder)
        {
            builder.ToTable("ARMs");
            builder.OwnsMany(e => e.NetworkAdapters, na => na.ToTable("NetworkAdapters"));
        }
    }
}
