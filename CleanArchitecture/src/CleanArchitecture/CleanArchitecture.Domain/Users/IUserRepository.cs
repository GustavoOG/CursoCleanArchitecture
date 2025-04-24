namespace CleanArchitecture.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

        void Add(User user);

        Task<User?> GetEmailAsync(Email email, CancellationToken cancellationToken = default);

        Task<bool> IsUserExists(Email email, CancellationToken cancellationToken = default);
    }
}
