using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Commands;

public record MassCreateRecipesCommand(IList<RecipeCreateDto> Recipes) : IRequest<OneOf<Success, Error>>, IValidate;