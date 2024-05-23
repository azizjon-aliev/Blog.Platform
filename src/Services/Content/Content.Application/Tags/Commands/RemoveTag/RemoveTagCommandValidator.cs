using Content.Application.Categories.Commands.RemoveCategory;
using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Tags.Commands.RemoveTag;

public class RemoveTagCommandValidator : AbstractValidator<RemoveCategoryCommand>
{
    public RemoveTagCommandValidator(ITagRepository repository)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) =>
            {
                var tagExists = await repository.ExistsAsync(p => p.Id == id, cancellationToken);
                return tagExists;
            }).WithMessage("Tag does not exist.");
    }
}