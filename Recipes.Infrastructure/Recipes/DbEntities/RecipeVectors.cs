using Recipes.Domain.Recipes.Models;
using Vector = Pgvector.Vector;

namespace Recipes.Infrastructure.Recipes.DbEntities;

public class RecipeVectors
{
    public Guid Id { get; set; }
    
    public Vector Vector { get; set; } = null!;

    public Guid RecipeId { get; set; }

    public string Recipe { get; set; } = null!;
}