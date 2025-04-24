using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquieres
{
    public static class AlquilerErrores
    {

        public static Error NotFound =
            new Error("Alquiler.Found", "El alquiler con el id especificado no fue encontrado");


        public static Error Overlap =
            new("Alquiler.Overlap", "El alquiler esta siendo tomado por 2 o mas clientes al mismo tiempo en la misma fecha");

        public static Error NotReserved =
            new("Alquiler.NotReserved", "El alquiler no esta reservado");

        public static Error NotConfirmed =
            new("Alquiler.NotConfirmed", "El alquiler no esta confirmado");

        public static Error AlreadyStarted =
            new("Alquiler.AlreadyStarted", "El alquiler ya ha comenzado");

    }
}
