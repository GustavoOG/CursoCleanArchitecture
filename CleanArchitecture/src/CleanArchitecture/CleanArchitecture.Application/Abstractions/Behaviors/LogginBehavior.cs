using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CleanArchitecture.Application.Abstractions.Behaviors
{
    public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
        where TResponse : Result
    {
        private readonly ILogger<LogginBehavior<TRequest, TResponse>> _logger;

        public LogginBehavior(ILogger<LogginBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;
            try
            {
                _logger.LogInformation($"Ejecutando el Request: {name}", name);
                var result = await next();

                if (result.IsSuccess)
                {
                    _logger.LogInformation($"El request {name} fue exitoso", name);
                }
                else
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        _logger.LogError($"El request {name} tuvo errores", name);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"El request {name} tuvo errores", name);
                throw;
            }
        }
    }
}
