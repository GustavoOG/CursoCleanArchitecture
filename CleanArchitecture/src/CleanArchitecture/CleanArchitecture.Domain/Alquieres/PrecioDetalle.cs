using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquieres
{
    public record PrecioDetalle(
        Moneda PrecioPorPeriodo,
        Moneda Mantenimiento,
        Moneda Accesorios,
        Moneda PrecioTotal
        );
}
