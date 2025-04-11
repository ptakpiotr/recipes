using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Validators;

public class IngredientEditValidator : AbstractValidator<UpdateIngredientCommand>
{
    public IngredientEditValidator()
    {
        RuleFor(x => x.Ingredient.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Ingredient.Order).Must((x) => x is null or > 0);
    }
}