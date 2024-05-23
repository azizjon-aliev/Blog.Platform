using Content.Application.Common.Interfaces.Repositories;
using FluentValidation;

namespace Content.Application.Tags.Commands.AddTag;

public class AddTagCommandValidator : AbstractValidator<AddTagCommand>
{
    public AddTagCommandValidator(ITagRepository repository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(5).WithMessage("Name must be at least 5 characters.")
            .MaximumLength(200).WithMessage("Name must not exceed 100 characters.")
            .MustAsync(async (name, cancellationToken) =>
            {
                var tagExists = await repository.ExistsAsync(p => p.Name == name, cancellationToken);
                return !tagExists;
            }).WithMessage("Name must be unique.");
    }
}