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
            // Define Primary Key
            builder.HasKey(c => c.Id);

            // Configure UserId as required
            builder.Property(c => c.UserId)
                   .IsRequired();

            // Store OnlineTime and OfflineTime as JSON in MySQL
            builder.Property(c => c.OnlineTime)
                   .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Convert to JSON string
                        v => JsonSerializer.Deserialize<List<DateTime>>(v, (JsonSerializerOptions?)null) // Convert back
                   )
                   .HasColumnType("JSON"); // Store as JSON in MySQL

            builder.Property(c => c.OfflineTime)
                   .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<DateTime>>(v, (JsonSerializerOptions?)null)
                   )
                   .HasColumnType("JSON");

            // (Optional) Set Table Name Explicitly
            builder.ToTable("ComingAndgoings");


        }
    }
}
