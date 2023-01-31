using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SP23.P01.Web.Features {
    public class TrainStationConfiguration : IEntityTypeConfiguration<TrainStation> {
        public void Configure(EntityTypeBuilder<TrainStation> builder) {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(120);
            builder
                .Property(x => x.Address)
                .IsRequired();

        }
    }
}
