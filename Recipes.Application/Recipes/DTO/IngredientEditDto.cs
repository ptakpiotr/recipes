namespace Recipes.Application.Recipes.DTO;

public class IngredientEditDto
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public string? Description { get; set; }

    public int? Order { get; set; }
}