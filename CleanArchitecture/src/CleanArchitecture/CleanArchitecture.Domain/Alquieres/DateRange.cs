namespace CleanArchitecture.Domain.Alquieres
{
    public sealed record DateRange
    {
        private DateRange()
        {
        }

        public DateOnly Inicio { get; init; }
        public DateOnly Fin { get; init; }

        public int CantidadDias => Fin.DayNumber - Inicio.DayNumber;

        public static DateRange Create(DateOnly inicio, DateOnly fin)
        {
            if (inicio > fin)
            {
                throw new ApplicationException("La fecha fin no puede ser menor a la fecha inicio");
            }
            return new DateRange { Inicio = inicio, Fin = fin };
        }

    }
}
