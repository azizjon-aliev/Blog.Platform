using MediatR;

namespace Content.Application.Posts.Queries.GetPostById;

public class GetPostByIdQuery: IRequest<PostDetailVm>
{
    public int Id { get; set; }
}