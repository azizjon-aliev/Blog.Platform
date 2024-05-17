using System.Linq.Expressions;
using Content.Application.Common.Extensions;
using Content.Application.Common.Interfaces;
using Content.Application.Common.Interfaces.Repositories;
using Content.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Content.Infrastructure.DataProvider.Repositories;

public class PostRepository(IContentDbContext dbContext): IPostRepository
{
    public async Task<List<Post>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Posts.ToListAsync(cancellationToken);
    }

    public async Task<Post> AddAsync(Post post, CancellationToken cancellationToken)
    {
        await dbContext.Posts.AddAsync(post, cancellationToken: cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return post;
    }

    public async Task<Post> EditAsync(Guid id, Post post, CancellationToken cancellationToken)
    {
        var existingPost = await dbContext.Posts.FindAsync(id, cancellationToken);
        
        if (existingPost == null)
        {
            throw new NotFoundException(nameof(Category), id);
        }

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
        existingPost.CategoryId = post.CategoryId;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return existingPost;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingPost = await dbContext.Posts.FindAsync(id, cancellationToken);
        
        if (existingPost == null)
        {
            throw new NotFoundException(nameof(Category), id);
        }
        
        dbContext.Posts.Remove(existingPost);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken)
    {
        return await dbContext.Posts.AnyAsync(predicate, cancellationToken);
    }
}