using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users
{
    public static class VehiculoErrors
    {
        public static Error NotFound = new(
            "User.Found", "No existe el vehiculo buscado");

    }
}
