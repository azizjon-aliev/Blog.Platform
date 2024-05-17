using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Posts.Commands.EditPost;

public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
{
    public EditPostCommandValidator(IPostRepository repository, ICategoryRepository categoryRepository)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var categoryExists = await repository.ExistsAsync(p => p.Id == id, cancellationToken);
                return categoryExists;
            }).WithMessage("Post does not exist.");

        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
            .MaximumLength(200).WithMessage("Title must not exceed 100 characters.")
            .MustAsync(async (title, cancellationToken) =>
            {
                var postExists = await repository.ExistsAsync(p => p.Title == title, cancellationToken);
                return !postExists;
            }).WithMessage("Title must be unique.");

        RuleFor(p => p.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MinimumLength(10).WithMessage("Content must be at least 10 characters.")
            .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.")
            .MustAsync(async (categoryId, cancellationToken) =>
            {
                var categoryExists = await categoryRepository.ExistsAsync(p => p.Id == categoryId, cancellationToken);
                return categoryExists;
            }).WithMessage("Category does not exist.");
    }
}