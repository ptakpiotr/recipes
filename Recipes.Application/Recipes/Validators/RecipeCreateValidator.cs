using Recipes.Application.Recipes.Commands;

namespace Recipes.Application.Recipes.Validators;

public class RecipeCreateValidator : AbstractValidator<CreateRecipeCommand>
{
    public RecipeCreateValidator()
    {
        RuleFor(x => x.Recipe.AuthorId).NotEqual(Guid.Empty);
        RuleFor(x => x.Recipe.Title).NotEmpty();
        RuleFor(x => x.Recipe.Description).NotEmpty();
        RuleFor(x => x.Recipe.Image).NotNull();
        RuleFor(x => x.Recipe.Types).NotEmpty();
        RuleFor(x => x.Recipe.Ingredients).NotEmpty();
    }
}