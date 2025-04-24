using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehiculos.Specifications
{
    public class VehiculoPaginationSpecification : BaseSpecification<Vehiculo, VehiculoId>
    {

        public VehiculoPaginationSpecification(string sort, int pageIndex, int pageSize, string search) :
            base(x => string.IsNullOrEmpty(search) || x.Modelo == new Modelo(search))
        {
            ApplyPaging(pageSize * (pageIndex - 1) * pageSize, pageSize);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "modeloAsc": AddOrderBy(x => x.Modelo!); break;
                    case "modeloDEsc": AddOrderByDescending(x => x.Modelo!); break;
                    default: AddOrderBy(x => x.FechaUltimaAlquiler!); break;
                }
            }
            else
            {
                AddOrderBy(x => x.FechaUltimaAlquiler!);
            }
        }
    }

}
