using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Handlers;
using Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers;

public class CreateRatingHandlerTests(RatingsService services) : RatingsServiceFixture
{
    [Fact]
    public async Task CreateIngredient_ShouldReturnSuccess()
    {
        CreateRatingHandler handler = new(services.SuccessRatingsService);

        var param = new CreateRatingCommand(new()
        {
            RecipeId = Guid.NewGuid(),
            Rating = 4
        });

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<RatingReadDto>);
    }

    [Fact]
    public async Task CreateIngredient_ShouldReturnFailure()
    {
        CreateRatingHandler handler = new(services.FailureRatingsService);

        var param = new CreateRatingCommand(new()
        {
            RecipeId = Guid.NewGuid(),
            Rating = 4
        });

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}