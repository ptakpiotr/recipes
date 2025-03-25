using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Validators;
using Recipes.Application.UnitTests.Recipes.Validators.Fixtures;

namespace Recipes.Application.UnitTests.Recipes.Validators;

public class IngredientEditValidatorTests(IngredientEditValidator validator) : IngredientEditValidatorFixture
{
    [Theory]
    [MemberData(nameof(ValidIngredientData))]
    public void ValidIngredientData_ShouldReturnTrue(Guid id, int order)
    {
        var dto = new IngredientEditDto()
        {
            Id = id,
            Order = order
        };

        var res = validator.Validate(dto);

        Assert.True(res.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidIngredientData))]
    public void InvalidIngredientData_ShouldReturnFalse(Guid id, int order)
    {
        var dto = new IngredientEditDto()
        {
            Id = id,
            Order = order
        };

        var res = validator.Validate(dto);

        Assert.False(res.IsValid);
    }

    public static IEnumerable<object[]> ValidIngredientData()
    {
        yield return [Guid.NewGuid(), 2];
        yield return [Guid.NewGuid(), 25];
    }

    public static IEnumerable<object[]> InvalidIngredientData()
    {
        yield return [Guid.Empty, 10];
        yield return [Guid.NewGuid(), -1];
        yield return [Guid.Empty, -1];
    }
}