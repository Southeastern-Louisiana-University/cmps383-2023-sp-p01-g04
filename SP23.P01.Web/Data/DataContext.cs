using System;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Data
{
    public class DataContext : DbContext
    {
        //"super"
        //dbcontextoptions and pass it to bass class
        //db context...because you want it to say use this specfic connection string.
        //our class has to konw
        //only constructed using options, nothing else, no other constructors?
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<TrainStation> TrainStations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //this is like adding <Trainstation, but all of them
            //finds the class and puts it in. gets the type and searches and finds all iEntity types
            // public class trainstationconfiuration: iEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // this stores all decimal values to two decimal points by default - good enough for our purposes
            configurationBuilder.Properties<decimal>()
                .HavePrecision(18, 2);
        }

    }
}

