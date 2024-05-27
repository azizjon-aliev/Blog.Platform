using System.Linq.Expressions;
using Identity.Domain.Entities;

namespace Identity.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User> AddAsync(User category, CancellationToken cancellationToken);

    public Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}