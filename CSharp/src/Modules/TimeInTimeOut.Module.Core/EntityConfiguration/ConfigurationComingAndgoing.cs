using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.EntityConfiguration
{
    public class ConfigurationComingAndgoing : IEntityTypeConfiguration<ComingAndgoing>
    {
        public void Configure(EntityTypeBuilder<ComingAndgoing> builder)
        {
            builder.HasKey(x => x.Id);
           

            builder.HasMany(x => x.OflineTime).
            WithOne()
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.OnlineTime).
            WithOne()
            .OnDelete(DeleteBehavior.SetNull);

           
        }
    }
}
