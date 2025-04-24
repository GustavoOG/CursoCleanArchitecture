namespace CleanArchitecture.Domain.Alquieres
{

    public record AlquilerId(Guid Value)
    {
        public static AlquilerId New() => new(Guid.NewGuid());
    }
}
