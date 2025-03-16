using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Queries;

public record GetRecipeByIdQuery(Guid RecipeId) : IRequest<OneOf<SuccessWithValue<RecipeReadDto>, Error>>;
