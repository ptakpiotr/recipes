using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Commands;

public record UpdateIngredientCommand(IngredientEditDto Ingredient, Guid UserId) : IRequest<OneOf<CommandStatus, Error>>;