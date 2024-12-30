using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TimeInTimeOut.Module.Core.EntityConfiguration
{
    public class ComingAndgoing : IEntityTypeConfiguration<ComingAndgoing>
    {
        public void Configure(EntityTypeBuilder<ComingAndgoing> builder)
        {
            throw new NotImplementedException();
        }
    }
}
