using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class NetworkHardwareConfiguration : IEntityTypeConfiguration<NetworkHardware>
    {
        public void Configure(EntityTypeBuilder<NetworkHardware> builder)
        {
            builder.ToTable("NetworkHardwares");
            builder.OwnsMany(e => e.NetworkDevices, nd => nd.ToTable("NetworkDevices"));
        }
    }
}
