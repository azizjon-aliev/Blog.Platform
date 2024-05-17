using Content.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Content.Application.Common.Interfaces;

public interface IContentDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Post> Posts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}