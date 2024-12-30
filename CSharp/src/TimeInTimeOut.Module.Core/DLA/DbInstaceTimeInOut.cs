
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TimeInTimeOut.Module.Core.Domain.Entity;

namespace TimeInTimeOut.Module.Core.DLA
{
    public class DbInstaceTimeInOut : DbContext
    {
        public DbInstaceTimeInOut(DbContextOptions<DbInstaceTimeInOut> options) : base(options)
        {
        }
        public DbSet<ComingAndgoing>? TimeInOuts { get; set; }
        public DbSet<DateTimeTimeInTimeOut>? DateTimeTimeInTimeOuts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }


         public class DbInstaceBreakeFactory : IDesignTimeDbContextFactory<DbInstaceTimeInOut>
    {
        public DbInstaceTimeInOut CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("ConnectionStringsDB.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbInstaceTimeInOut>();
            optionsBuilder.UseMySql(
                   "Server=localhost;Port=3306;Database=Feature;User=root;Password=password;",
                    new MySqlServerVersion(new Version(8, 0, 30))
                );

            return new DbInstaceTimeInOut(optionsBuilder.Options);
        }
    }

    }
}