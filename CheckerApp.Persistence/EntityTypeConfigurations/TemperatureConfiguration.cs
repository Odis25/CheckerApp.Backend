using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class TemperatureConfiguration : IEntityTypeConfiguration<Temperature>
    {
        public void Configure(EntityTypeBuilder<Temperature> builder)
        {
            builder.ToTable("Temperatures");
        }
    }
}
