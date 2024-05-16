using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Categories.Commands.AddCategory;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator(ICategoryRepository repository)
    {
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