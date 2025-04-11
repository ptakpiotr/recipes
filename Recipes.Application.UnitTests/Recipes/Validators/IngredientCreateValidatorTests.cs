using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Validators;
using Recipes.Application.UnitTests.Recipes.Validators.Fixtures;

namespace Recipes.Application.UnitTests.Recipes.Validators;

public class IngredientCreateValidatorTests(IngredientCreateValidator validator) : IngredientCreateValidatorFixture
{
    [Theory]
    [MemberData(nameof(GenerateValidIngredientsData))]
    public void ValidIngredients_ShouldReturnTrue(Guid recipeId, string desc, int order)
    {
        var dto = new IngredientCreateDto()
        {
            RecipeId = recipeId,
            Description = desc,
            Order = order
        };
        
        var res = validator.Validate(new CreateIngredientCommand(dto));

        Assert.True(res.IsValid);
    }
    
    [Theory]
    [MemberData(nameof(GenerateInvalidIngredientsData))]
    public void InvalidIngredients_ShouldReturnFalse(Guid recipeId, string desc, int order)
    {
        var dto = new IngredientCreateDto()
        {
            RecipeId = recipeId,
            Description = desc,
            Order = order
        };
        
        var res = validator.Validate(new CreateIngredientCommand(dto));

        Assert.False(res.IsValid);
    }

    public static IEnumerable<object[]> GenerateValidIngredientsData()
    {
        yield return [Guid.NewGuid(), "Description", 1];
        yield return [Guid.NewGuid(), "Description 2", 2];
    }
    
    public static IEnumerable<object[]> GenerateInvalidIngredientsData()
    {
        yield return [Guid.Empty, "Description", 1];
        yield return [Guid.NewGuid(), string.Empty, 2];
        yield return [Guid.NewGuid(), "Description 2", -1];
        yield return [Guid.Empty, string.Empty, -1];
    }
}