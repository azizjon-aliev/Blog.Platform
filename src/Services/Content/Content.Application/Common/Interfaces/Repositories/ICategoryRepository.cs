using System.Linq.Expressions;
using Content.Domain.Entities;

namespace Content.Application.Common.Interfaces.Repositories;

public interface ICategoryRepository
{
    public Task<Category> AddAsync(Category category, CancellationToken cancellationToken);

    public Task<Category> EditAsync(int id, Category category, CancellationToken cancellationToken);

    public Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);

    public Task<bool> ExistsAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);

    public Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken);
}