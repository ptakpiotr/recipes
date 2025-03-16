namespace Recipes.Application.Recipes.DTO;

public class IngredientReadDto
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public string Description { get; set; } = null!;
    
    public int Order { get; set; }
}