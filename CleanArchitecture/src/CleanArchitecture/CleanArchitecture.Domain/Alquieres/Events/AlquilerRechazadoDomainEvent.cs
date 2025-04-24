using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquieres.Events
{
    public sealed record AlquilerRechazadoDomainEvent(AlquilerId AlquilerId) : IDomainEvent;
}