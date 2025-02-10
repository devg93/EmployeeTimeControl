
using Microsoft.EntityFrameworkCore.Design;

namespace TimeInTimeOut.Module.Core.DAL
{
    public class DbInstaceTimeInOut : DbContext
    {
        public DbInstaceTimeInOut(DbContextOptions<DbInstaceTimeInOut> options) : base(options)
        {
        }
        public DbSet<ComingAndgoing>?  comingAndgoings { get; set; }
 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbInstaceTimeInOut).Assembly);
            base.OnModelCreating(modelBuilder);
        }


         public class DbInstaceBreakeFactory : IDesignTimeDbContextFactory<DbInstaceTimeInOut>
    {
        public DbInstaceTimeInOut CreateDbContext(string[] args)
        {
            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("ConnectionStringsDB.json")
            //     .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbInstaceTimeInOut>();
            optionsBuilder.UseMySql(
                   "Server=localhost;Port=3306;Database=TimeInTimeOut;User=root;Password=password;",
                    new MySqlServerVersion(new Version(8, 0, 30))
                );

            return new DbInstaceTimeInOut(optionsBuilder.Options);
        }
    }

    }
}