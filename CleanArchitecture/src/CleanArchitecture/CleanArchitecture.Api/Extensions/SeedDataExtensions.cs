using Bogus;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infraestructure;
using Dapper;

namespace CleanArchitecture.Api.Extensions
{
    public static class SeedDataExtensions
    {

        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();
            List<object> vehiculos = new();
            for (var i = 0; i < 100; i++)
            {
                vehiculos.Add(new
                {
                    Id = Guid.NewGuid(),
                    Vin = faker.Vehicle.Vin(),
                    Modelo = faker.Vehicle.Model(),
                    Pais = faker.Address.Country(),
                    Calle = faker.Address.StreetName(),
                    Colonia = "colonia",
                    Ciudad = faker.Address.City(),
                    Municipio = "municipio",
                    Estado = faker.Address.State(),
                    PrecioMonto = faker.Random.Decimal(1000, 20000),
                    PrecioTipoMoneda = "USD",
                    PrecioMantenimiento = faker.Random.Decimal(100, 200),
                    PrecioMantenimientoTipoMoneda = "USD",
                    Accesorios = new List<int> { (int)Accesorio.Wifi, (int)Accesorio.AppleCar },
                    FechaUltima = DateTime.MinValue
                });
            }

            const string sql = """
                INSERT INTO public.vehiculos(
                id, modelo, vin, direccion_calle, direccion_colonia, direccion_ciudad, direccion_municipio, direccion_estado, direccion_pais, precio_monto, precio_tipo_moneda, mantenimiento_monto, mantenimiento_tipo_moneda, fecha_ultima_alquiler, accesorios)
                VALUES (@Id, @Modelo, @Vin, @Calle, @Colonia, @Ciudad,
                @Municipio, @Estado, @Pais, @PrecioMonto, @PrecioTipoMoneda, 
                @PrecioMantenimiento, @PrecioMantenimientoTipoMoneda, @FechaUltima, @Accesorios);
                """;

            connection.Execute(sql, vehiculos);

        }

        public static void SeedDataAuthentication(this IApplicationBuilder app)
        {

            using var scope = app.ApplicationServices.CreateScope();
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = service.GetRequiredService<ApplicationDbContext>();
                if (!context.Set<User>().Any())
                {
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword("ADMIN123*");

                    var user = User.Create(
                        new Nombre("GUSTAVO"),
                        new Apellido("ORTIZ GAMEZ"),
                        new Email("jdxtavo@gmail.com"),
                        new PasswordHash(passwordHash));
                    context.Add(user);

                    passwordHash = BCrypt.Net.BCrypt.HashPassword("ADMIN123*");
                    user = User.Create(
                        new Nombre("ADMIN"),
                        new Apellido("ADMIN"),
                        new Email("ADMIN@gmail.com"),
                        new PasswordHash(passwordHash));
                    context.Add(user);
                    context.SaveChangesAsync().Wait();

                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
