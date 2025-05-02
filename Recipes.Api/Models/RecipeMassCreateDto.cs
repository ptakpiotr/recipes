using Microsoft.AspNetCore.Mvc.ModelBinding;
using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Api.Models;

public class RecipeMassCreateDto
{
    [BindNever] public Guid AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int[] Image { get; set; } = null!;

    public string ImageName { get; set; } = null!;

    public ICollection<RecipeType> Types { get; set; } = [];

    public ICollection<IngredientCreateDto> Ingredients { get; set; } = [];
}