using Recipes.Application.Recipes.Commands;

namespace Recipes.Application.Recipes.Validators;

public class RecipeEditValidator : AbstractValidator<UpdateRecipeCommand>
{
    public RecipeEditValidator()
    {
        RuleFor(x => x.Recipe.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Recipe.AuthorId).NotEqual(Guid.Empty);
        RuleFor(x => x.Recipe.Types).NotEmpty();
        RuleFor(x => x.Recipe.Ingredients).NotEmpty();
    }
}