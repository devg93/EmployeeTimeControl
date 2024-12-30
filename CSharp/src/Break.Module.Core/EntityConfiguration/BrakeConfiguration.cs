

using Break.Module.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Break.Module.Core.EntityConfiguration
{
    public class BrakeConfiguration : IEntityTypeConfiguration<BrakeTime>
    {
        public void Configure(EntityTypeBuilder<BrakeTime> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}