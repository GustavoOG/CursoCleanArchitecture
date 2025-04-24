using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquieres.Events
{
    public sealed record AlquilerCompletadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
}