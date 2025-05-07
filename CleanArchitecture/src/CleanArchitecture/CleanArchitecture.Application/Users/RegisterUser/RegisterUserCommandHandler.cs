using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.RegisterUser
{
    internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //1. Validar que el usuario sea unico por correo
            var userExists = await _userRepository.IsUserExists(new Email(request.Email), cancellationToken);
            if (userExists)
            {
                return Result.Failure<Guid>(UserErrors.AlreadyExists);
            }
            //2. crear un objeto tipo usuario
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            //3. crear un objeto de tipo user
            var user = User.Create(
                new Nombre(request.Nombre),
                new Apellido(request.Apellidos),
                new Email(request.Email),
                new PasswordHash(passwordHash)
                );

            //4. Insetar el usuario en la base de datos
            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user.Id!.Value;
        }
    }
}
