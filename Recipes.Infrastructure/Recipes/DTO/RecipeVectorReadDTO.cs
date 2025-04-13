using Pgvector;

namespace Recipes.Infrastructure.Recipes.DTO;

public class RecipeVectorReadDTO
{
    public Guid Id { get; set; }

    public Vector Vector { get; set; } = null!;

    public Guid RecipeId { get; set; }
}