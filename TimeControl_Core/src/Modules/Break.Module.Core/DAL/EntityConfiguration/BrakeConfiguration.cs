
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Break.Module.Core.Entity;

namespace Modules.Break.Module.Core.EntityConfiguration.DAL;


 //************************************Add Entity BrakeTime Configuration ********************************************//
public sealed class  BrakeConfiguration : IEntityTypeConfiguration<BrakeTime>
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
            .HasMany(ws => ws.BrakeStartTime)
            .WithOne(s=>s.BrakeTimeStart)
            .HasForeignKey(s => s.BrakeId)
            .OnDelete(DeleteBehavior.SetNull);


        builder
            .HasMany(ws => ws.BrakeEndTime)
            .WithOne(e=>e.BrakeTimeEnd)
            .HasForeignKey(e => e.BrakeId)
            .OnDelete(DeleteBehavior.SetNull);


    }

 //************************************Add Entity DateTimeWorkSchedule Configuration ********************************************//
     public class ConfigurationDateTimeWorkSchedule : IEntityTypeConfiguration<DateTimeWorkSchedule>
    {
        public void Configure(EntityTypeBuilder<DateTimeWorkSchedule> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.StartTime)
                   .IsRequired(false);

            builder.Property(t => t.EndTime)
                   .IsRequired(false); 
        }
    }
}
