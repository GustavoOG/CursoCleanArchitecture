using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Application.Abstranctions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Users.LoginUser
{
    internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //1. Verificar que usuario exista en D
            //Buscar un usuario en la bd por el email
            var user = await _userRepository.GetEmailAsync(new Email(request.Email), cancellationToken);
            if (user is null)
            {
                return Result.Failure<string>(UserErrors.NotFound);
            }
            //2. Verificar que la contraseña sea correcta
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash!.Value))
            {
                return Result.Failure<string>(UserErrors.InvalidCredentials);
            }
            //3. Generar JWT
            var token = await _jwtProvider.Generate(user);
            //4. return JWT;
            return token;
        }
    }
}
