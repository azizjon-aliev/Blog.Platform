using Content.Application.Categories.Queries.GetCategoryById;
using Content.Application.Tags.Queries.GetTagById;
using MediatR;

namespace Content.Application.Tags.Commands.EditTag;

public class EditTagCommand : IRequest<TagDetailVm>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}