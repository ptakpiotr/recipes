using Recipes.Domain.Common.Abstractions;

namespace Recipes.Domain.Recipes.Models;

public class IngredientModel : IStorableEntity
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public string Description { get; set; } = null!;
    
    public int Order { get; set; }

    public RecipeModel Recipe { get; set; } = null!;
}