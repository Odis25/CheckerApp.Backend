using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class FlowmeterConfiguration : IEntityTypeConfiguration<Flowmeter>
    {
        public void Configure(EntityTypeBuilder<Flowmeter> builder)
        {
            builder.ToTable("Flowmeters");
            builder.OwnsOne(e => e.Settings, ms => ms.ToTable("FlowmeterSettings"));
        }
    }
}
