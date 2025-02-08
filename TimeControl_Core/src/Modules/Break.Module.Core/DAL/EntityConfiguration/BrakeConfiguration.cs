
//************Add Entity BrakeTime Configuration Witch IEntityTypeConfiguration <T>********//
namespace Modules.Break.Module.Core.EntityConfiguration.DAL;
public sealed class BrakeConfiguration : IEntityTypeConfiguration<BrakeTime>
{
    public void Configure(EntityTypeBuilder<BrakeTime> builder)
    {
        builder.HasKey(ws => ws.Id);


        builder
            .HasOne(ws => ws.busyChecker)
            .WithOne()
            .HasForeignKey<BrakeTime>(ws => ws.busyId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}