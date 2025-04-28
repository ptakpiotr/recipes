namespace Recipes.Application.Recipes.DTO;

public class IngredientCreateDto
{
    public string Description { get; set; } = null!;

    public int Order { get; set; }
}