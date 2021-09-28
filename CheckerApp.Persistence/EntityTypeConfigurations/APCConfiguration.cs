using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class APCConfiguration : IEntityTypeConfiguration<APC>
    {
        public void Configure(EntityTypeBuilder<APC> builder)
        {
            builder.ToTable("APCs");
        }
    }
}
