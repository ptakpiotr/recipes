using NSubstitute;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;

public class RatingsService
{
    public IRatingsService SuccessRatingsService { get; } = Substitute.For<IRatingsService>();

    public IRatingsService FailureRatingsService { get; } = Substitute.For<IRatingsService>();

    public RatingsService()
    {
        #region success

        SuccessRatingsService.GetRatingsForRecipeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IReadOnlyList<RatingReadDto>>([]));

        SuccessRatingsService.CreateRatingForRecipeAsync(Arg.Any<RatingCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<RatingReadDto>(new()));

        #endregion

        #region failure

        FailureRatingsService.GetRatingsForRecipeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureRatingsService.CreateRatingForRecipeAsync(Arg.Any<RatingCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));
        
        #endregion
    }
}

public class RatingsServiceFixture : IClassFixture<RatingsService>
{
}