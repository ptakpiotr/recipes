using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.Recipes.Queries;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class GetRecipeByIdHandlerTests(RecipesServices services) : RecipesServiceFixture
{
    [Fact]
    public async Task GetRecipeById_ShouldReturnSuccess()
    {
        GetRecipeByIdHandler handler = new(services.SuccessRecipeService);

        var param = new GetRecipeByIdQuery(Guid.NewGuid());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<RecipeReadDto>);
    }

    [Fact]
    public async Task GetRecipeById_ShouldReturnFailure()
    {
        GetRecipeByIdHandler handler = new(services.FailureRecipeService);

        var param = new GetRecipeByIdQuery(Guid.NewGuid());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}