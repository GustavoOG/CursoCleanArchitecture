﻿using Asp.Versioning;
using CleanArchitecture.Api.Utils;
using CleanArchitecture.Application.Vehiculos.GetVehiculosByPagination;
using CleanArchitecture.Application.Vehiculos.GetVehiculosKitByPagination;
using CleanArchitecture.Application.Vehiculos.ReportVehiculoPdf;
using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using System.Net;

namespace CleanArchitecture.Api.Controllers.Vehiculos
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/vehiculos")]
    public class VehiculosController : ControllerBase
    {
        public readonly ISender _sender;

        public VehiculosController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpGet("reporte")]
        public async Task<IActionResult> ReporteVehiculos(CancellationToken cancellationToken, string modelo = "")
        {
            var query = new ReportVehiculoPdfQuery(modelo);
            var resultados = await _sender.Send(query, cancellationToken);
            byte[] pdfBytes = resultados.Value.GeneratePdf();
            return File(pdfBytes, "application/pdf");
        }



        [HashPermission(PermissionEnum.ReadUser)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchVehiculos(
            DateOnly StartDate, DateOnly EndDate, CancellationToken cancellationToken)
        {
            var query = new SearchVehiculosQuery(StartDate, EndDate);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getPagination", Name = "PaginationVehiculos")]
        [ProducesResponseType(typeof(PaginationResult<Vehiculo, VehiculoId>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationResult<Vehiculo, VehiculoId>>> GetPaginationVehiculo
            ([FromQuery] GetVehiculosByPaginationQuery request)
        {
            var resultados = await _sender.Send(request);
            return Ok(resultados);
        }

        [AllowAnonymous]
        [HttpGet("getPaginationKit", Name = "GetPaginationVehiculoKit")]
        [ProducesResponseType(typeof(PaginationResult<Vehiculo, VehiculoId>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationResult<Vehiculo, VehiculoId>>> GetPaginationVehiculoKit
        ([FromQuery] GetVehiculosKitByPaginationQuery request)
        {
            var resultados = await _sender.Send(request);
            return Ok(resultados);
        }
    }
}
