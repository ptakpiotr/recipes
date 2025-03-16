using Recipes.Domain.Common.Abstractions;

namespace Recipes.Domain.Recipes.Models;

public class RatingModel : IStorableEntity
{
    public Guid Id { get; set; }

    public int Rating { get; set; }
    
    public Guid RecipeId { get; set; }

    public Guid UserId { get; set; }
}