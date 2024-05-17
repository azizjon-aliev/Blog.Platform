using MediatR;

namespace Content.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<CategoryDetailVm>
{
    public Guid Id { get; set; }
}