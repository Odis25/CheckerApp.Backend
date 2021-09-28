using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class DiffPressureConfiguration : IEntityTypeConfiguration<DiffPressure>
    {
        public void Configure(EntityTypeBuilder<DiffPressure> builder)
        {
            builder.ToTable("DiffPressures");
        }
    }
}
