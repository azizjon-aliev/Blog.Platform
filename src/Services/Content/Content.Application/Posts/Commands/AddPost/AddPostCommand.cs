using AutoMapper;
using Content.Application.Common.Mappings;
using Content.Application.Posts.Queries.GetPostById;
using Content.Domain.Entities;
using MediatR;

namespace Content.Application.Posts.Commands.AddPost;

public class AddPostCommand : IRequest<PostDetailVm>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid CategoryId { get; set; }
}