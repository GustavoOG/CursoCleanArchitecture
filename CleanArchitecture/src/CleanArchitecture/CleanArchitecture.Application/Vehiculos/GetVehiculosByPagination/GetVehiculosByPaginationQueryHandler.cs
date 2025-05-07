using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Vehiculos.Specifications;

namespace CleanArchitecture.Application.Vehiculos.GetVehiculosByPagination
{
    internal sealed class GetVehiculosByPaginationQueryHandler :
        IQueryHandler<GetVehiculosByPaginationQuery, PaginationResult<Vehiculo, VehiculoId>>
    {

        private readonly IVehiculoRepository _vehiculoRepository;

        public GetVehiculosByPaginationQueryHandler(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }


        public async Task<Result<PaginationResult<Vehiculo, VehiculoId>>> Handle(
            GetVehiculosByPaginationQuery request, CancellationToken cancellationToken)
        {
            var spec = new VehiculoPaginationSpecification(request.Sort!, request.PageIndex, request.PageSize, request.Modelo!);

            var records = await _vehiculoRepository.GetAllWithSpec(spec);

            var specCount = new VehiculoPaginationCountingSpecification(request.Modelo!);
            var totalrecords = await _vehiculoRepository.CountAsync(specCount);

            var totalPages = (int)(Math.Ceiling((decimal)totalrecords / (decimal)request.PageSize));
            var recordsByPage = records.Count();

            return new PaginationResult<Vehiculo, VehiculoId>
            {
                Count = totalrecords,
                Data = records.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = recordsByPage
            };
        }
    }
}
