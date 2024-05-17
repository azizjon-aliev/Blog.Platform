using AutoMapper;
using Content.Application.Common.Extensions;
using Content.Application.Common.Interfaces.Repositories;
using Content.Domain.Entities;
using MediatR;

namespace Content.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDetailVm>
{
    public async Task<CategoryDetailVm> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var dbResponse = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (dbResponse is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        return mapper.Map<CategoryDetailVm>(dbResponse);
    }
}