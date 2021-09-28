using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class PressureConfiguration : IEntityTypeConfiguration<Pressure>
    {
        public void Configure(EntityTypeBuilder<Pressure> builder)
        {
            builder.ToTable("Pressures");
        }
    }
}
