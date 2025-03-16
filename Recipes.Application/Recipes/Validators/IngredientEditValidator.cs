using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Validators;

public class IngredientEditValidator : AbstractValidator<IngredientEditDto>
{
    public IngredientEditValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Order).Must((x) => x is null or > 0);
    }
}