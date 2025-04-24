using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.Property(user => user.Id)
                .HasConversion(userId => userId!.Value, value => new UserId(value));

            builder.Property(user => user.Nombre)
                .HasMaxLength(200)
                .HasConversion(nombre => nombre!.Value, value => new Nombre(value));

            builder.Property(user => user.Apellido)
                .HasMaxLength(200)
                .HasConversion(apellido => apellido!.Value, value => new Apellido(value));

            builder.Property(user => user.Email)
               .HasMaxLength(200)
               .HasConversion(email => email!.Value, value => new Domain.Users.Email(value));

            builder.Property(user => user.PasswordHash)
               .HasMaxLength(2000)
               .HasConversion(passwordhash => passwordhash!.Value, value => new Domain.Users.PasswordHash(value));

            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasMany(user => user.Roles).WithMany().UsingEntity<UserRole>();
        }
    }
}
