using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Mappers;

public class IngredientsMappersProfile : Profile
{
    public IngredientsMappersProfile()
    {
        CreateMap<IngredientCreateDto, IngredientModel>();
        CreateMap<IngredientModel, IngredientReadDto>();
    }
}