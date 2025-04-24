using CleanArchitecture.Application.Abstranctions.Messaging;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    public record ReservarAlquilerCommand(
        Guid VehiculoId,
        Guid UserId,
        DateOnly FechaInicio,
        DateOnly Fechafin) : ICommand<Guid>;
}
