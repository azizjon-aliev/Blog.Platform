using Content.Application.Tags.Queries.GetTagById;
using MediatR;

namespace Content.Application.Tags.Commands.AddTag;

public class AddTagCommand : IRequest<TagDetailVm>
{
    public string Name { get; set; }
}