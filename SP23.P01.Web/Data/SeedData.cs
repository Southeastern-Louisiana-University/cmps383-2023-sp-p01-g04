using System;
using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Data
{
	
    public static class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<DataContext>();
            context.Database.Migrate();

            AddTrainStations(context);
        }

        private static void AddTrainStations(DataContext context)
        {
            var trainStations = context.Set<TrainStation>();
            if (trainStations.Any())
            {
                return;
            }

            trainStations.Add(new TrainStation
            {
                Name = "Hammond Station",
                Address = "123 Hammond St"
            });
            trainStations.Add(new TrainStation
            {
                Name = "Baton Rouge Station",
                Address = "456 Rouge Ln"
            });
            trainStations.Add(new TrainStation
            {
                Name = "New Orleans Station",
                Address = "789 Nola Ave"
            });
            context.SaveChanges();
        }
    }
}

