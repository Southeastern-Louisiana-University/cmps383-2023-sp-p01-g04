using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Features;

namespace SP23.P01.Web.Data
{
    public static class SeedData {
        public static void Initialize(IServiceProvider services) {
            var context = services.GetRequiredService<DataContext>();
            context.Database.Migrate();

            AddTrainStations(context);
        }

        private static void AddTrainStations(DataContext context) {
            var products = context.Set<TrainStation>();
            if (products.Any()) {
                return;
            }

            products.Add(new TrainStation {
                Name = "Hammond station",
                Address="123 Hammond St"
            });
            products.Add(new TrainStation {
                Name = "Baton Rouge Station",
                Address= "456 Rouge Ln"
            });
            products.Add(new TrainStation {
                Name = "New Orleans station",
                Address= "789 Nola Ave"
            });
            context.SaveChanges();
        }
    }
}
