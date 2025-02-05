using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TimeInTimeOut.Module.Core.Domain.Entity;

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
