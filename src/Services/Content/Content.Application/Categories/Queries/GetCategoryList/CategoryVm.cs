using AutoMapper;
using Content.Application.Common.Mappings;
using Content.Domain.Entities;

namespace Content.Application.Categories.Queries.GetCategoryList;

public class CategoryVm : IMapWith<Category>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryVm>();
    }
}