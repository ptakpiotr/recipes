using System.Text.Json.Serialization;

namespace Recipes.Domain.Recipes.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RecipeType
{
    Sweet,
    Dinner,
    Breakfast,
    Lunch,
    Other
}