using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Validators;
using Recipes.Application.UnitTests.Recipes.Validators.Fixtures;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.UnitTests.Recipes.Validators;

public class RecipeEditValidatorTests(RecipeEditValidator validator) : RecipeEditValidatorFixture
{
    [Theory]
    [MemberData(nameof(ValidIngredientData))]
    public void ValidIngredientData_ShouldReturnTrue(Guid id, Guid authorId, List<RecipeType> recipes)
    {
        var dto = new RecipeEditDto()
        {
            Id = id,
            AuthorId = authorId,
            Types = recipes,
            Ingredients = []
        };

        var res = validator.Validate(dto);

        Assert.True(res.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidIngredientData))]
    public void InvalidIngredientData_ShouldReturnFalse(Guid id, Guid authorId, List<RecipeType> recipes)
    {
        var dto = new RecipeEditDto()
        {
            Id = id,
            AuthorId = authorId,
            Types = recipes,
            Ingredients = []
        };

        var res = validator.Validate(dto);

        Assert.False(res.IsValid);
    }

    public static IEnumerable<object[]> ValidIngredientData()
    {
        yield return [Guid.NewGuid(), Guid.NewGuid(), new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), Guid.NewGuid(), new List<RecipeType>() { new RecipeType() }];
    }

    public static IEnumerable<object[]> InvalidIngredientData()
    {
        yield return [Guid.Empty, Guid.NewGuid(), new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), Guid.Empty, new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), Guid.NewGuid(), new List<RecipeType>() { }];
    }
}