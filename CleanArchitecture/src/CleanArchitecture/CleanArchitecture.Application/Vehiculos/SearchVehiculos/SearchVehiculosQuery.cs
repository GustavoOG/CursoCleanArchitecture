using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    public record SearchVehiculosQuery(DateOnly fechaInicio, DateOnly fechafin) : IQuery<IReadOnlyList<VehiculoResponse>>;

}
