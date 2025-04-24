using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstranctions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquieres;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos
{
    internal sealed class SearchVehiculoQueryHandler : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
    {
        private static readonly int[] ActiveAlquilerStatuses =
            {
            (int)AlquilerStatus.Reservado,
            (int)AlquilerStatus.Completado,
            (int)AlquilerStatus.Confirmado
        };
        private readonly ISqlConnectionFactory _connectionFactory;

        public SearchVehiculoQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(SearchVehiculosQuery request, CancellationToken cancellationToken)
        {
            if (request.fechaInicio > request.fechafin)
            {
                return new List<VehiculoResponse>();
            }
            using var connection = _connectionFactory.CreateConnection();
            const string sql = """
                select 
                a.id AS Id,
                a.modelo as Modelo,
                a.vin as Vin,
                a.precio_monto as Precio,
                a.precio_tipo_moneda as TipoMoneda,
                a.direccion_calle as Calle,
                a.direccion_colonia as Colonia,
                a.direccion_ciudad as Ciudad,
                a.direccion_municipio as  Municipio,
                a.direccion_estado as  Estado,
                a.direccion_pais as  Pais

                FROM vehiculos AS a
                WHERE NOT EXISTS (
                SELECT 1 FROM alquileres AS b
                WHERE b.vehiculo_id = a.id and
                b.duracion_inicio <= @EndDate and
                b.duracion_fin >= @StarDate and
                b.status = ANY(@ActiveAlquilerStatuses)
                )
                """;

            var vehiculos = await connection.QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>(
                sql,
                (vehiculo, direccion) =>
                {
                    vehiculo.Direccion = direccion;
                    return vehiculo;
                }, new
                {
                    StarDate = request.fechaInicio,
                    EndDate = request.fechafin,
                    ActiveAlquilerStatuses
                },
               splitOn: "Calle"
                );

            return vehiculos.ToList();
        }
    }
}

