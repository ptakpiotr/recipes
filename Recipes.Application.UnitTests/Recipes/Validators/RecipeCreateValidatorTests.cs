using Microsoft.AspNetCore.Http;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Validators;
using Recipes.Application.UnitTests.Recipes.Validators.Fixtures;
using Recipes.Domain.Recipes.Enums;

namespace Recipes.Application.UnitTests.Recipes.Validators;

public class RecipeCreateValidatorTests(RecipeCreateValidator validator) : RecipeCreateValidatorFixture
{
    [Theory]
    [MemberData(nameof(ValidIngredientData))]
    public void ValidIngredientData_ShouldReturnTrue(Guid authorId, string title, string desc, string image, List<RecipeType> recipes)
    {
        var dto = new RecipeCreateDto()
        {
           AuthorId = authorId,
           Title = title,
           Description = desc,
           Types = recipes,
           Image = new FormFile(Stream.Null, 0, 0, image, ""),
           Ingredients = []
        };

        var res = validator.Validate(new CreateRecipeCommand(dto));

        Assert.True(res.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidIngredientData))]
    public void InvalidIngredientData_ShouldReturnFalse(Guid authorId, string title, string desc, string image, List<RecipeType> recipes)
    {
        var dto = new RecipeCreateDto()
        {
            AuthorId = authorId,
            Title = title,
            Description = desc,
            Types = recipes,
            Image = new FormFile(Stream.Null, 0, 0, image, ""),
            Ingredients = []
        };

        var res = validator.Validate(new CreateRecipeCommand(dto));

        Assert.False(res.IsValid);
    }

    public static IEnumerable<object[]> ValidIngredientData()
    {
        yield return [Guid.NewGuid(), "Title", "Desc", "Image", new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), "Title 2", "Desc 2", "Image 2", new List<RecipeType>() { new RecipeType() }];
    }

    public static IEnumerable<object[]> InvalidIngredientData()
    {
        yield return [Guid.Empty, "Title", "Desc", "Image", new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), string.Empty, "Desc", "Image", new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), "Title", string.Empty, "Image", new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), "Title", "Desc", string.Empty, new List<RecipeType>() { new RecipeType() }];
        yield return [Guid.NewGuid(), "Title", "Desc", "Image", new List<RecipeType>() {}];
        yield return [Guid.Empty, string.Empty, string.Empty, string.Empty, new List<RecipeType>() {}];
    }
}