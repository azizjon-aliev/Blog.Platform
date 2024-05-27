using AutoMapper;
using BlogPlatform.Service.Common.Mappings;
using Identity.Domain.Entities;

namespace Identity.Application.Users.Responses;

public class UserDetailVm : IMapWith<User>
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDetailVm>().ReverseMap();
    }
}