using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Categories.Commands.EditCategory;

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator(ICategoryRepository repository)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var categoryExists = await repository.ExistsAsync(p => p.Id == id, cancellationToken);
                return categoryExists;
            }).WithMessage("Category does not exist.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(5).WithMessage("Name must be at least 5 characters.")
            .MaximumLength(200).WithMessage("Name must not exceed 100 characters.")
            .MustAsync(async (name, cancellationToken) =>
            {
                var categoryExists = await repository.ExistsAsync(p => p.Name == name, cancellationToken);
                return !categoryExists;
            }).WithMessage("Name must be unique.");
    }
}