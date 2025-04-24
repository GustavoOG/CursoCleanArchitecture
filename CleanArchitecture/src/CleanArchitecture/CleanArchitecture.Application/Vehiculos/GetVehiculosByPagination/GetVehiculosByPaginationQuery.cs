using CleanArchitecture.Application.Abstranctions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Vehiculos.GetVehiculosByPagination
{
    public sealed record GetVehiculosByPaginationQuery : SpecificationEntry,
        IQuery<PaginationResult<Vehiculo, VehiculoId>>
    {

        public string? Modelo { get; init; }
    }
}
