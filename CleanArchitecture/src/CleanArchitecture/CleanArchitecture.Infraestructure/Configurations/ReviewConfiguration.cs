using CleanArchitecture.Domain.Alquieres;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(review => review.Id);

            builder.Property(review => review.Id)
                .HasConversion(reviewId => reviewId!.Value, value => new ReviewId(value));

            builder.Property(review => review.Rating)
                .HasConversion(review => review!.Value, value => Rating.Create(value).Value);

            builder.Property(review => review.Comentario)
                .HasMaxLength(200)
                .HasConversion(comentario => comentario!.value, value => new Comentario(value));

            builder.HasOne<Vehiculo>()
             .WithMany()
             .HasForeignKey(review => review.VehiculoId);

            builder.HasOne<Alquiler>()
                .WithMany()
                .HasForeignKey(review => review.AlguilerId);

            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(review => review.UserId);
        }
    }
}
