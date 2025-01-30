using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.EntityConfiguration.DAL
{
    public class ConfigurationComingAndgoing : IEntityTypeConfiguration<ComingAndgoing>
    {
        public void Configure(EntityTypeBuilder<ComingAndgoing> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.OnlineTime)
                   .WithOne(t => t.OnlineComingAndgoing)
                   .HasForeignKey(t => t.ComingAndgoingId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.OfflineTime)
                   .WithOne(t => t.OfflineComingAndgoing)
                   .HasForeignKey(t => t.ComingAndgoingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ConfigurationDateTimeTimeInTimeOut : IEntityTypeConfiguration<DateTimeTimeInTimeOut>
    {
        public void Configure(EntityTypeBuilder<DateTimeTimeInTimeOut> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TimeIn)
                   .IsRequired(false);

            builder.Property(t => t.TimeOut)
                   .IsRequired(false); 
        }
    }
}
