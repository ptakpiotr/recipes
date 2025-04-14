using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.Recipes.Queries;

public record GetAllRecipesQuery(RecipeType? RecipeType) : IRequest<OneOf<SuccessWithValue<IReadOnlyList<RecipeReadDto>>, Error>>;