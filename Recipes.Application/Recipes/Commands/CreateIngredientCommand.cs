using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Commands;

public record CreateIngredientCommand(IngredientCreateDto Ingredient)
    : IRequest<OneOf<SuccessWithValue<IngredientReadDto>, Error>>, IValidate;
