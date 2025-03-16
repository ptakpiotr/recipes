using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.Recipes.DTO;

public class RecipeEditDto
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public ICollection<RecipeType> Types { get; set; } = [];

    public DateTimeOffset UpdatedAt { get; set; }
    
    public ICollection<IngredientEditDto> Ingredients { get; set; } = [];
}