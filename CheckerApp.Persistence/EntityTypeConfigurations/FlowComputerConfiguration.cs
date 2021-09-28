using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class FlowComputerConfiguration : IEntityTypeConfiguration<FlowComputer>
    {
        public void Configure(EntityTypeBuilder<FlowComputer> builder)
        {
            builder.ToTable("FlowComputers");
        }
    }
}
