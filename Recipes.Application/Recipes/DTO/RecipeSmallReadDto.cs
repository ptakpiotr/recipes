using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.Recipes.DTO;

public class RecipeSmallReadDto
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public ICollection<RecipeType> Types { get; set; } = [];
}