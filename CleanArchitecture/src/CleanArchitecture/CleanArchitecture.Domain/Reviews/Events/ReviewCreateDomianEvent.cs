using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Reviews;

namespace CleanArchitecture.Domain.Alquileres.Events
{
    public sealed record ReviewCreateDomianEvent(ReviewId ReviewId) : IDomainEvent;
}