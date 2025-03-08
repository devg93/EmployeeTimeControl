
namespace TimeInTimeOut.Module.Core.EntityConfiguration.DAL
{
    public class ConfigurationComingAndgoing : IEntityTypeConfiguration<ComingAndgoing>
    {
        public void Configure(EntityTypeBuilder<ComingAndgoing> builder)
        {
         
            builder.HasKey(c => c.Id);

        
            builder.Property(c => c.UserId)
                   .IsRequired();


        }
    }
}
