using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Mappers;

public class RatingsMappersProfile : Profile
{
    public RatingsMappersProfile()
    {
        CreateMap<RatingCreateDto, RatingModel>();
        CreateMap<RatingModel, RatingReadDto>();
    }
}