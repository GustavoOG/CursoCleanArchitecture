using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using CleanArchitecture.Domain.Permissions;
using MediatR;

namespace CleanArchitecture.Api.Controllers.Alquileres
{
    public static class AlquileresEndpoints
    {
        public static IEndpointRouteBuilder MapAlquilerEndpoints(this IEndpointRouteBuilder builder)
        {

            builder.MapGet("alquileres/{id}", GetAlquiler)
                .RequireAuthorization(PermissionEnum.ReadUser.ToString())
                .WithName(nameof(GetAlquiler));

            builder.MapPost("alquileres", ReservaAlquiler)
                .RequireAuthorization(PermissionEnum.writeUser.ToString());

            return builder;
        }

        public static async Task<IResult> GetAlquiler(Guid id, ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetAlquilerQuery(id);
            var resultado = await sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Results.Ok(resultado.Value) : Results.NotFound();
        }

        public static async Task<IResult> ReservaAlquiler(AlquilerReservaRequest request, ISender _sender, CancellationToken cancellationToken)
        {
            var command = new ReservarAlquilerCommand(request.VehiculoId, request.UserId, request.StartDate, request.EndDate);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.CreatedAtRoute(nameof(GetAlquiler), new { id = result.Value }, result.Value);
        }
    }
}
