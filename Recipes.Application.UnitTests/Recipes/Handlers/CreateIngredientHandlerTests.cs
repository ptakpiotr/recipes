using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class CreateIngredientHandlerTests(IngredientsServices services) : IngredientsServiceFixtures
{
    [Fact]
    public async Task CreateIngredient_ShouldReturnSuccess()
    {
        CreateIngredientHandler handler = new(services.SuccessIngredientsService);

        var param = new CreateIngredientCommand(new()
        {
            RecipeId = Guid.NewGuid(),
            Description = "Desc",
            Order = 1
        });

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<IngredientReadDto>);
    }

    [Fact]
    public async Task CreateIngredient_ShouldReturnFailure()
    {
        CreateIngredientHandler handler = new(services.FailureIngredientsService);

        var param = new CreateIngredientCommand(new()
        {
            RecipeId = Guid.NewGuid(),
            Description = "Desc",
            Order = 1
        });

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}