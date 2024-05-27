using AutoMapper;
using Identity.Application.Common.Interfaces.Repositories;
using Identity.Application.Users.Responses;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Users.Commands.AddUser;

public class AddUserCommandHandler(IUserRepository repository, IMapper mapper)
    : IRequestHandler<AddUserCommand, UserDetailVm>
{
    public async Task<UserDetailVm> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var newUser = await repository.AddAsync(user, cancellationToken);

        return mapper.Map<UserDetailVm>(newUser);
    }
}