using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquieres.Events
{
    public sealed record AlquilerReservadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
}
