using Content.Application.Categories.Queries.GetCategoryById;
using Content.Application.Tags.Queries.GetTagById;
using MediatR;

namespace Content.Application.Categories.Commands.EditCategory;

public class EditCategoryCommand : IRequest<CategoryDetailVm>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}