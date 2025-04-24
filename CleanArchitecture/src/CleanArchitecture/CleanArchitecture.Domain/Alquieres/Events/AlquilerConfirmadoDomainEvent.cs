using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquieres.Events
{
    public sealed record AlquilerConfirmadoDomainEvent(AlquilerId Id) : IDomainEvent;

}
