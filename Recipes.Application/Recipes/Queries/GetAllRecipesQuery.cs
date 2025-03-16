using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Queries;

public record GetAllRecipesQuery() : IRequest<OneOf<SuccessWithValue<IReadOnlyList<RecipeReadDto>>, Error>>;