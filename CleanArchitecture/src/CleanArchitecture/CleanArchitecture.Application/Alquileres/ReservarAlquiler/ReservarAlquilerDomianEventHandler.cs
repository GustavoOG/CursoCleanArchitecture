using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Alquieres;
using CleanArchitecture.Domain.Alquieres.Events;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Alquileres.ReservarAlquiler
{
    internal class ReservarAlquilerDomianEventHandler
        : INotificationHandler<AlquilerReservadoDomainEvent>
    {
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public ReservarAlquilerDomianEventHandler(IAlquilerRepository alquilerRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _alquilerRepository = alquilerRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(AlquilerReservadoDomainEvent notification, CancellationToken cancellationToken)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(notification.AlquilerId, cancellationToken);
            if (alquiler is null)
            {
                return;
            }
            var user = await _userRepository.GetByIdAsync(alquiler.UserId!, cancellationToken);
            if (user is null)
            {
                return;
            }

            await _emailService.SendAsync(user.Email!, "Alquiler Reservado",
                $"Por Favor confirma tu reserva, Id de la reservacion: {alquiler.Id}");
        }
    }
}
