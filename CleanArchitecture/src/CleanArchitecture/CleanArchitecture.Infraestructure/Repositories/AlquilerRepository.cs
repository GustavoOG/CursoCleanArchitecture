using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories
{
    internal sealed class AlquilerRepositor : Repository<Alquiler, AlquilerId>, IAlquilerRepository
    {
        private static readonly AlquilerStatus[] activealquilerstatues = { AlquilerStatus.Reservado, AlquilerStatus.Confirmado, AlquilerStatus.Completado };

        public AlquilerRepositor(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsOverlappingAsync(Vehiculo vehiculo, DateRange duracion, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Alquiler>()
                .AnyAsync(
                alquiler =>
                alquiler.VehiculoId == vehiculo.Id &&
                alquiler.Duracion!.Inicio <= duracion.Fin &&
                alquiler.Duracion.Fin >= duracion.Inicio &&
                activealquilerstatues.Contains(alquiler.Status), cancellationToken
                );

        }
    }
}
