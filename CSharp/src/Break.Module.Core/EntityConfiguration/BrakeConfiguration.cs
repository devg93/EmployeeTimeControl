

using Break.Module.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Break.Module.Core.EntityConfiguration
{
    public class BrakeConfiguration : IEntityTypeConfiguration<BrakeTime>
    {
        public void Configure(EntityTypeBuilder<BrakeTime> builder)
        {
            builder.HasKey(ws => ws.Id);

           
            builder
                .HasOne(ws => ws.busyChecker)
                .WithOne() 
                .HasForeignKey<BrakeTime>(ws => ws.busyId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder
                .HasMany(ws => ws.StartTime)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);
         

            builder
                .HasMany(ws => ws.EndTime)
                .WithOne() 
                .OnDelete(DeleteBehavior.SetNull);
           

        }
    }
}