using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Events;

public record SendNewsletterDataEvent(RecipeModel Recipe);