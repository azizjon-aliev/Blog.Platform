using System.Linq.Expressions;
using Identity.Application.Common.Interfaces;
using Identity.Application.Common.Interfaces.Repositories;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.DataProvider.Repositories;

public class UserRepository(IApplicationDbContext context) : IUserRepository
{
    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Users.AnyAsync(predicate, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users.FindAsync(id, cancellationToken);
    }
}