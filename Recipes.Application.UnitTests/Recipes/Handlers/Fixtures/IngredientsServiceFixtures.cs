using NSubstitute;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;

public class IngredientsServices
{
    public IIngredientsService SuccessIngredientsService { get; } = Substitute.For<IIngredientsService>();

    public IIngredientsService FailureIngredientsService { get; } = Substitute.For<IIngredientsService>();

    public IngredientsServices()
    {
        #region success

        SuccessIngredientsService.GetIngredientsForRecipeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IReadOnlyList<IngredientReadDto>>([]));

        SuccessIngredientsService.CreateIngredientAsync(Arg.Any<IngredientCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IngredientReadDto>(new IngredientReadDto()));

        SuccessIngredientsService
            .UpdateIngredientAsync(Arg.Any<IngredientEditDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Success());
        
        SuccessIngredientsService
            .DeleteIngredientAsync(Arg.Any<IngredientDeleteDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Success());

        #endregion

        #region failure

        FailureIngredientsService.GetIngredientsForRecipeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureIngredientsService.CreateIngredientAsync(Arg.Any<IngredientCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureIngredientsService
            .UpdateIngredientAsync(Arg.Any<IngredientEditDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));
        
        FailureIngredientsService
            .DeleteIngredientAsync(Arg.Any<IngredientDeleteDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));
        
        #endregion
    }
}

public class IngredientsServiceFixtures : IClassFixture<IngredientsServices>
{
}