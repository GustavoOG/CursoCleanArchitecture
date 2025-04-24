using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquieres.Events
{
    public sealed record AlquilerCanceladoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
}