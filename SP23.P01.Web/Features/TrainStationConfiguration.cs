using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SP23.P01.Web.Features {
    public class TrainStationConfiguration : IEntityTypeConfiguration<TrainStation> {
        public void Configure(EntityTypeBuilder<TrainStation> builder) {
        }
    }
}
