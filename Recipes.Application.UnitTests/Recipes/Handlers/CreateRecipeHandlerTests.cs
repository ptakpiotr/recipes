using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class CreateRecipeHandlerTests(RecipesServices services) : RecipesServiceFixture
{
    [Fact]
    public async Task CreateRecipe_ShouldReturnSuccess()
    {
        CreateRecipeHandler handler = new(services.SuccessRecipeService);

        var param = new CreateRecipeCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<RecipeReadDto>);
    }

    [Fact]
    public async Task CreateRecipe_ShouldReturnFailure()
    {
        CreateRecipeHandler handler = new(services.FailureRecipeService);

        var param = new CreateRecipeCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}