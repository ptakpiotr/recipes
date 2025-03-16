namespace Recipes.Application.Recipes.DTO;

public class RatingCreateDto
{
    public int Rating { get; set; }
    
    public Guid RecipeId { get; set; }

    public Guid UserId { get; set; }
}