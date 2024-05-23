using AutoMapper;
using Content.Application.Common.Mappings;
using Content.Domain.Entities;

namespace Content.Application.Tags.Queries.GetTagList;

public class TagVm : IMapWith<Tag>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, TagVm>();
    }
}