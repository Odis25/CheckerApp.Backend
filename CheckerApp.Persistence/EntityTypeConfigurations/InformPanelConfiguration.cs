using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class InformPanelConfiguration : IEntityTypeConfiguration<InformPanel>
    {
        public void Configure(EntityTypeBuilder<InformPanel> builder)
        {
            builder.ToTable("InformPanels");
        }
    }
}
