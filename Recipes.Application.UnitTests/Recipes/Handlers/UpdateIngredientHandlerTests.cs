using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class UpdateIngredientHandlerTests(IngredientsServices services) : IngredientsServiceFixtures
{
    [Fact]
    public async Task UpdateIngredient_ShouldReturnSuccess()
    {
        UpdateIngredientHandler handler = new(services.SuccessIngredientsService);

        var param = new UpdateIngredientCommand(new(), Guid.Empty);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }

    [Fact]
    public async Task UpdateIngredient_ShouldReturnFailure()
    {
        UpdateIngredientHandler handler = new(services.FailureIngredientsService);

        var param = new UpdateIngredientCommand(new(), Guid.Empty);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }
}