﻿using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAlquilerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<AlquilerResponse>> Handle(GetAlquilerQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            const string sql = """
                                SELECT
                                     id AS Id,
                                     vehiculo_id AS VehiculoId,
                                     user_id AS UserId,
                                     status AS Status,
                                     precio_por_pediodo_monto AS PrecioAlquiler,
                                precio_por_pediodo_tipo_moneda AS TipoMonedaAlquiler,
                                     mantenimiento_monto AS PrecioMantenimiento,
                                     mantenimiento_tipo_moneda AS TipoMonedaMantenimiento,
                                     accesorios_monto AS AccesoriosPrecio,
                                     accesorios_tipo_moneda AS TipoMonedaAccesorio,
                                     precio_total_monto AS PrecioTotal,
                                     precio_total_tipo_moneda AS PrecioTotalTipoMoneda,
                                     duracion_inicio AS DuracionInicio,
                                     duracion_fin AS DuracionFinal,
                                     fecha_creacion AS FechaCreacion
                                FROM alquileres WHERE id=@AlquilerId  
                                """;
            var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
                            sql,
                            new
                            {
                                request.AlquilerId
                            }
                            );
            return alquiler!;

        }
    }
}

