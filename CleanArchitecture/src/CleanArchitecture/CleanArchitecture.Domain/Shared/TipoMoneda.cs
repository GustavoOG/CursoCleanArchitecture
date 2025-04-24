namespace CleanArchitecture.Domain.Shared
{
    public record TipoMoneda
    {
        public static readonly TipoMoneda None = new("");
        public static readonly TipoMoneda Usd = new("USD");
        public static readonly TipoMoneda Eur = new("EUR");
        public static readonly TipoMoneda Mxn = new("MXN");

        private TipoMoneda(string codigo) => Codigo = codigo;

        public string? Codigo { get; init; }

        public static readonly IReadOnlyCollection<TipoMoneda> All = new[] {
            Usd,
            Eur,
            Mxn
        };

        public static TipoMoneda FromCodigo(string codigo)
        {
            return All.FirstOrDefault(m => m.Codigo!.Equals(codigo))
                ?? throw new ApplicationException("El tipo de moneda no existe");
        }


    }
}
