using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Reviews;

namespace CleanArchitecture.Domain.Alquieres.Events
{
    public sealed record ReviewCreateDomianEvent(ReviewId ReviewId) : IDomainEvent;
}