using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Mappers;

public class RecipesMappersProfile : Profile
{
    public RecipesMappersProfile()
    {
        CreateMap<RecipeCreateDto, RecipeModel>();
        CreateMap<RecipeModel, RecipeReadDto>();
    }
}