using FluentValidation;
using Identity.Application.Common.Interfaces.Repositories;

namespace Identity.Application.Users.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator(IUserRepository repository)
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username обязательное поле")
            .MinimumLength(5).WithMessage("Username должен быть не менее 5 символов")
            .MaximumLength(200).WithMessage("Username должен быть не более 200 символов")
            .MustAsync(async (username, cancellationToken) =>
            {
                var exists = await repository.ExistsAsync(u => u.Username == username, cancellationToken);
                return !exists;
            }).WithMessage("Username уже существует");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email обязательное поле")
            .MinimumLength(5).WithMessage("Email должен быть не менее 5 символов")
            .MaximumLength(255).WithMessage("Email должен быть не более 255 символов")
            .EmailAddress().WithMessage("Email не валидный формат")
            .MustAsync(async (email, cancellationToken) =>
            {
                var exists = await repository.ExistsAsync(u => u.Email == email, cancellationToken);
                return !exists;
            }).WithMessage("Email уже существует");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password обязательное поле")
            .MinimumLength(8).WithMessage("Password должен быть не менее 8 символов")
            .MaximumLength(255).WithMessage("Password должен быть не более 255 символов");

        RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password).WithMessage("Пароли не совпадают");
    }
}