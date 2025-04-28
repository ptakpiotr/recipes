using Recipes.Application.Recipes.Commands;

namespace Recipes.Application.Recipes.Validators;

public class IngredientCreateValidator : AbstractValidator<CreateIngredientCommand>
{
    public IngredientCreateValidator()
    {
        RuleFor(x => x.Ingredient.Description).NotEmpty();
        RuleFor(x => x.Ingredient.Order).GreaterThan(0);
    }
}