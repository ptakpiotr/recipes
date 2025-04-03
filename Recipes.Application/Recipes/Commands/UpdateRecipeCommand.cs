using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Commands;

public record UpdateRecipeCommand(RecipeEditDto Recipe, Guid UserId) : IRequest<OneOf<CommandStatus, Error>>, IValidate;
