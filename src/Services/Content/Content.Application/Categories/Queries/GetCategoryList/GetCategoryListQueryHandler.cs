using AutoMapper;
using Content.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Content.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListQueryHandler(ICategoryRepository repository, IMapper mapper)
    : IRequestHandler<GetCategoryListQuery, List<CategoryVm>>
{
    public async Task<List<CategoryVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        var dbResponse = await repository.GetAllAsync(cancellationToken);
        return mapper.Map<List<CategoryVm>>(dbResponse);
    }
}