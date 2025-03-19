namespace Recipes.Application.Recipes.DTO;

public class IngredientDeleteDto
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }

}