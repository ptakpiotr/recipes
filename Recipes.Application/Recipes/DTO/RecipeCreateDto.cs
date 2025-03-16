using Microsoft.AspNetCore.Http;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.Recipes.DTO;

public class RecipeCreateDto
{
    public Guid AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public IFormFile Image { get; set; } = null!;

    public ICollection<RecipeType> Types { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<IngredientCreateDto> Ingredients { get; set; } = [];
}