using CleanArchitecture.Application.Alquileres.GetAlquiler;
using CleanArchitecture.Application.Alquileres.ReservarAlquiler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Alquileres
{
    [Route("api/alquileres")]
    [ApiController]
    public class AlquileresController : ControllerBase
    {
        public readonly ISender _sender;

        public AlquileresController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlquiler(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetAlquilerQuery(id);
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReservaAlquiler(Guid id, AlquilerReservaRequest request, CancellationToken cancellationToken)
        {
            var command = new ReservarAlquilerCommand(request.VehiculoId, request.UserId, request.StartDate, request.EndDate);
            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetAlquiler), new { id = result.Value });
        }
    }
}
