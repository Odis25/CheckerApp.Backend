using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class FireModuleConfiguration : IEntityTypeConfiguration<FireModule>
    {
        public void Configure(EntityTypeBuilder<FireModule> builder)
        {
            builder.ToTable("FireModules");
        }
    }
}
