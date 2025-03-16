using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Validators;

public class RecipeEditValidator : AbstractValidator<RecipeEditDto>
{
    public RecipeEditValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.AuthorId).NotEqual(Guid.Empty);
        RuleFor(x => x.Types).NotEmpty();
        RuleFor(x => x.Ingredients).NotEmpty();
    }
}