using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;

namespace Recipes.Application.UnitTests.Recipes;

public class UpdateRecipeHandlerTests(RecipesServices services) : RecipesServiceFixture
{
    [Fact]
    public async Task UpdateIngredient_ShouldReturnSuccess()
    {
        UpdateRecipeHandler handler = new(services.SuccessRecipeService);

        var param = new UpdateRecipeCommand(new(), Guid.Empty);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }

    [Fact]
    public async Task UpdateIngredient_ShouldReturnFailure()
    {
        UpdateRecipeHandler handler = new(services.FailureRecipeService);

        var param = new UpdateRecipeCommand(new(), Guid.Empty);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }
}