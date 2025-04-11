using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Recipes.Application.Recipes.DTO;

public class RatingCreateDto
{
    public int Rating { get; set; }
    public Guid RecipeId { get; set; }
    
    [BindNever]
    public Guid UserId { get; set; }
}