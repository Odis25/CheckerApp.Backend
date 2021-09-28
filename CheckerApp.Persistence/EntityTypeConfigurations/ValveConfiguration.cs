using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class ValveConfiguration : IEntityTypeConfiguration<Valve>
    {
        public void Configure(EntityTypeBuilder<Valve> builder)
        {
            builder.ToTable("Valves");
            builder.OwnsOne(e => e.Settings, ms => ms.ToTable("ValveSettings"));
        }
    }
}
