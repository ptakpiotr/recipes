using System.Text.Json.Serialization;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.Recipes.DTO;

public class RecipeReadDto
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public IList<RecipeType> Types { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public IList<IngredientReadDto> Ingredients { get; set; } = [];

    public IList<RatingReadDto> Ratings { get; set; } = [];
}