using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.Recipes.Queries;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class GetAllRecipesHandlerTests(RecipesServices services) : RecipesServiceFixture
{
    [Fact]
    public async Task GetAllRecipes_ShouldReturnSuccess()
    {
        GetAllRecipesHandler handler = new(services.SuccessRecipeService);

        var param = new GetAllRecipesQuery(null);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<IReadOnlyList<RecipeReadDto>>);
    }

    [Fact]
    public async Task GetAllRecipes_ShouldReturnFailure()
    {
        GetAllRecipesHandler handler = new(services.FailureRecipeService);

        var param = new GetAllRecipesQuery(null);

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}