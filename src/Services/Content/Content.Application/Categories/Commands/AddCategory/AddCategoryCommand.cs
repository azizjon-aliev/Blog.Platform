using Content.Application.Categories.Queries.GetCategoryById;
using MediatR;

namespace Content.Application.Categories.Commands.AddCategory;

public class AddCategoryCommand: IRequest<CategoryDetailVm>
{
    public string Name { get; set; }
}