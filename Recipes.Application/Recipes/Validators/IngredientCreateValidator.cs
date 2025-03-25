using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Validators;

public class IngredientCreateValidator : AbstractValidator<IngredientCreateDto>
{
    public IngredientCreateValidator()
    {
        RuleFor(x => x.RecipeId).NotEqual(Guid.Empty);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Order).GreaterThan(0);
    }
}