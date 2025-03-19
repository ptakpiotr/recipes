using Recipes.Domain.Common.Abstractions;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Domain.Recipes.Models;

public class RecipeModel : IStorableEntity
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<RecipeType> Types { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<IngredientModel> Ingredients { get; set; } = [];
    
    public ICollection<RatingModel> Ratings { get; set; } = [];
}