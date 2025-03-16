namespace Recipes.Application.Recipes.DTO;

public class RecipeDeleteDto
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }
}