using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(UserId userId) : IDomainEvent;

}
