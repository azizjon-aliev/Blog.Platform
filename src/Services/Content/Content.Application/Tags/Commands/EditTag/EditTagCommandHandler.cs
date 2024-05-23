using AutoMapper;
using Content.Application.Categories.Commands.EditCategory;
using Content.Application.Categories.Queries.GetCategoryById;
using Content.Application.Common.Interfaces.Repositories;
using Content.Application.Tags.Queries.GetTagById;
using Content.Domain.Entities;
using MediatR;

namespace Content.Application.Tags.Commands.EditTag;

public class EditTagCommandHandler(ITagRepository repository, IMapper mapper)
    : IRequestHandler<EditTagCommand, TagDetailVm>
{
    public async Task<TagDetailVm> Handle(EditTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Id = request.Id,
            Name = request.Name
        };
        var dbResponse = await repository.EditAsync(request.Id, tag, cancellationToken);
        return mapper.Map<TagDetailVm>(dbResponse);
    }
}