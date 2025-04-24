using FluentValidation;

namespace CleanArchitecture.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidation()
        {
            RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(c => c.Apellidos).NotEmpty().WithMessage("Los Apellidos son requerido");
            RuleFor(c => c.Email).EmailAddress().WithMessage("El email es incorrecto");
            RuleFor(c => c.Password).NotNull().WithMessage("El password no puede ser vacio")
                .MinimumLength(5).WithMessage("El password debe ser mayor a 5 caracteres");

        }

    }
}
