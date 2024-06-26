using AutoMapper;
using Content.Application.Common.Mappings;
using Content.Domain.Entities;

namespace Content.Application.Posts.Queries.GetPostList;

public class PostVm : IMapWith<Post>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt));
    }
}