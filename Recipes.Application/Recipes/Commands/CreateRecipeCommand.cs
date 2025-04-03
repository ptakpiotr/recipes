using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Commands;

public record CreateRecipeCommand(RecipeCreateDto Recipe) : IRequest<OneOf<SuccessWithValue<RecipeReadDto>, Error>>, IValidate;