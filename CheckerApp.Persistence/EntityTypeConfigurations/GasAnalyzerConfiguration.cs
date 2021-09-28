using CheckerApp.Domain.Entities.HardwareEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckerApp.Persistence.EntityTypeConfigurations
{
    public class GasAnalyzerConfiguration : IEntityTypeConfiguration<GasAnalyzer>
    {
        public void Configure(EntityTypeBuilder<GasAnalyzer> builder)
        {
            builder.ToTable("GasAnalyzers");
        }
    }
}
