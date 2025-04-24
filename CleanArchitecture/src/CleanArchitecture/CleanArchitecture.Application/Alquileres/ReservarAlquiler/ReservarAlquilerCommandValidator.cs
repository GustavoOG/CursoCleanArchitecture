using FluentValidation;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    public class ReservarAlquilerCommandValidator : AbstractValidator<ReservarAlquilerCommand>
    {
        public ReservarAlquilerCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("El Id del usuario es requerido");
            RuleFor(c => c.VehiculoId).NotEmpty().WithMessage("El Id del vehiculo es requerido");
            RuleFor(c => c.FechaInicio).LessThan(c => c.Fechafin).WithMessage("La fecha de inicio no pude ser menor a la fecha final");
        }
    }
}
