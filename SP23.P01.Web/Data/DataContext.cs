using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Features;

public class DataContext : DbContext {

    public DataContext(DbContextOptions<DataContext> options) : base(options) {

    }

    public DbSet<TrainStation> trainStations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder ) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TrainStationConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        base.ConfigureConventions(configurationBuilder);

        // this stores all decimal values to two decimal points by default - good enough for our purposes
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 2);
    }
}