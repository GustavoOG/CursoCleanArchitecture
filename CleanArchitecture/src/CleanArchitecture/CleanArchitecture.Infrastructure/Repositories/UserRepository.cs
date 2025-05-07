using CleanArchitecture.Application.Paginations;
using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal sealed class UserRepository :
        Repository<User, UserId>, IUserRepository, IPaginationRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> IsUserExists(Domain.Users.Email email, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<User>()
                .AnyAsync(x => x.Email == email, cancellationToken);
        }

        public override void Add(User user)
        {
            foreach (var role in user.Roles!)
            {
                DbContext.Attach(role);
            }

            DbContext.Add(user);
        }
    }
}
