using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Posts.Commands.AddPost;

public class AddPostCommandValidator : AbstractValidator<AddPostCommand>
{
    public AddPostCommandValidator(IPostRepository postRepository, ICategoryRepository categoryRepository)
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
            .MaximumLength(200).WithMessage("Title must not exceed 100 characters.")
            .MustAsync(async (title, cancellationToken) =>
            {
                var postExists = await postRepository.ExistsAsync(p => p.Title == title, cancellationToken);
                return !postExists;
            }).WithMessage("Title must be unique.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category is required.")
            .MustAsync(async (categoryId, cancellationToken) =>
            {
                var categoryExists = await categoryRepository.ExistsAsync(c => c.Id == categoryId, cancellationToken);
                return categoryExists;
            }).WithMessage("Category does not exist.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required")
            .MinimumLength(10).WithMessage("Content must be at least 10 characters.")
            .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");
    }
}