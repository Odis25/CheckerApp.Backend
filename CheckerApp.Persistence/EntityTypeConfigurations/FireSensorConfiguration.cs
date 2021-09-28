using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class FireSensorConfiguration : IEntityTypeConfiguration<FireSensor>
    {
        public void Configure(EntityTypeBuilder<FireSensor> builder)
        {
            builder.ToTable("FireSensors");
        }
    }
}
