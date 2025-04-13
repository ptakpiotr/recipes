using Pgvector;

namespace Recipes.Infrastructure.Recipes.DTO;

public class RecipeVectorModifyDTO
{
    public Vector Vector { get; set; } = null!;

    public Guid RecipeId { get; set; }
}