using CleanArchitecture.Domain.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.Abstranctions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
