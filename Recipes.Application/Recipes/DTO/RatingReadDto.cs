namespace Recipes.Application.Recipes.DTO;

public class RatingReadDto
{
    public Guid Id { get; set; }

    public int Rating { get; set; }
    
    public Guid RecipeId { get; set; }

    public Guid UserId { get; set; }
}