using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquieres
{
    public class PrecioService
    {

        public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
        {

            var tipoMoneda = vehiculo.Precio!.TipoMoneda;
            var precioPorPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio.Monto
                , tipoMoneda);

            decimal porcentaheChange = 0;

            foreach (var accesorio in vehiculo.Accesorios)
            {
                porcentaheChange += accesorio switch
                {
                    Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                    Accesorio.AireAcondicionado => 0.01m,
                    Accesorio.Mapas => 0.01m,
                    _ => 0m
                };
            }

            var accesorioChanges = Moneda.Zero(tipoMoneda);
            if (porcentaheChange > 0)
            {
                accesorioChanges = new Moneda(precioPorPeriodo.Monto * porcentaheChange, tipoMoneda);
            }
            var precioTotal = Moneda.Zero();
            precioTotal += precioPorPeriodo;

            if (!vehiculo!.Mantenimiento!.IsZero())
            {
                precioTotal += vehiculo.Mantenimiento;
            }

            precioTotal += accesorioChanges;

            return new PrecioDetalle(precioPorPeriodo, vehiculo.Mantenimiento, accesorioChanges, precioTotal);
        }
    }
}
