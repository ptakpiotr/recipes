using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class DeleteRecipeHandlerTests(RecipesServices services) : RecipesServiceFixture
{
    [Fact]
    public async Task DeleteRecipes_ShouldReturnSuccess()
    {
        DeleteRecipeHandler handler = new(services.SuccessRecipeService);

        var param = new DeleteRecipeCommand(new(), Guid.Empty);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }

    [Fact]
    public async Task DeleteRecipes_ShouldReturnFailure()
    {
        DeleteRecipeHandler handler = new(services.FailureRecipeService);

        var param = new DeleteRecipeCommand(new(), Guid.Empty);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }
}