namespace Recipes.Api.Models;

public class RecipesMassPayload
{
    public IList<RecipeMassCreateDto> Recipes { get; set; } = [];
}