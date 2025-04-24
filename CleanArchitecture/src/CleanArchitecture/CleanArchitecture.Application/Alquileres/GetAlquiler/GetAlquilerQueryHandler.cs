using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstranctions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.GetAlquiler
{
    internal sealed class GetAlquilerQueryHandler : IQueryHandler<GetAlquilerQuery, AlquilerResponse>
    {
        private readonly ISqlConnectionFactory? sqlConnectionFactory;
        public async Task<Result<AlquilerResponse>> Handle(GetAlquilerQuery request, CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory!.CreateConnection();
            const string sql = """
                                Select
                                id as Id,
                                vehiculo_Id as VahiculoId,
                                user_id as UserId,
                                status as Status,
                                precio_por_periodo as PrecioAlquiler,
                                precio_por_periodo_tipo_moneda as TipoMonedaAlquiler, 
                                precio_mantenimiento as PrecioMantenimiento, 
                                precio_manteniminto_tipo_moneda as TipoMonedaMantenimiento, 
                                precio_accesorios as AccesoriosPrecio,
                                precio_accesorios_tipo_mondea as TipoMonedaAccesorios, 
                                precio_total as PrecioTotal, 
                                precio_total_tipo_moneda as PrecioTotalTipoMoneda, 
                                duracion_inicio as DuracionInicio, 
                                duracion_final as DuracionFinal, 
                                fecha_creacion as  FechaCreacion 
                                from alquileres where id = @AlquilerId
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

