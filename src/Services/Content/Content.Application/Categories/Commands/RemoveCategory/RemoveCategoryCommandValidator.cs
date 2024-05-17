using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Categories.Commands.RemoveCategory;

public class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveCategoryCommandValidator(ICategoryRepository repository)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var categoryExists = await repository.ExistsAsync(p => p.Id == id, cancellationToken);
                return categoryExists;
            }).WithMessage("Category does not exist.");
    }
}