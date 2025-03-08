
namespace Modules.Break.Module.Core.DAL;

public class DbInstace : DbContext
{
    public DbInstace(DbContextOptions<DbInstace> options) : base(options)
    {
    }
    public DbSet<BrakeTime>? BrakeTimes { get; set; }
    public DbSet<busyChecker>? BusyCheckers { get; set; }
    public DbSet<DateTimeWorkSchedule>? DateTimeWorkSchedules { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbInstace).Assembly);
            base.OnModelCreating(modelBuilder);
    }

}


public class DbInstaceBreakeFactory : IDesignTimeDbContextFactory<DbInstace>
{
    public DbInstace CreateDbContext(string[] args)
    {
        // var configuration = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile("ConnectionStringsDB.json")
        //     .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DbInstace>();
        optionsBuilder.UseMySql(
               "Server=localhost;Port=3306;Database=Break;User=root;Password=password;",
                new MySqlServerVersion(new Version(8, 0, 30))
            );

        return new DbInstace(optionsBuilder.Options);
    }
}
