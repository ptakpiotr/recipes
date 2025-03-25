using NSubstitute;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Recipes.Handlers.Fixtures;

public class RecipesServices
{
    public IRecipeService SuccessRecipeService { get; } = Substitute.For<IRecipeService>();

    public IRecipeService FailureRecipeService { get; } = Substitute.For<IRecipeService>();

    public RecipesServices()
    {
        #region success

        SuccessRecipeService.GetRecipeByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<RecipeReadDto>(new RecipeReadDto()));

        SuccessRecipeService.GetRandomRecipeAsync(Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<RecipeReadDto>(new RecipeReadDto()));

        SuccessRecipeService.GetAllRecipesAsync(Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IReadOnlyList<RecipeReadDto>>([new RecipeReadDto()]));

        SuccessRecipeService.CreateRecipeAsync(Arg.Any<RecipeCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<RecipeReadDto>(new RecipeReadDto()));

        SuccessRecipeService.UpdateRecipeAsync(Arg.Any<RecipeEditDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Success());
        
        SuccessRecipeService.DeleteRecipeAsync(Arg.Any<RecipeDeleteDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Success());
        
        #endregion

        #region failure
        
        FailureRecipeService.GetRecipeByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureRecipeService.GetRandomRecipeAsync(Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureRecipeService.GetAllRecipesAsync(Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureRecipeService.CreateRecipeAsync(Arg.Any<RecipeCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        FailureRecipeService.UpdateRecipeAsync(Arg.Any<RecipeEditDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));
        
        FailureRecipeService.DeleteRecipeAsync(Arg.Any<RecipeDeleteDto>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Operation failed"));

        #endregion
    }
}

public class RecipesServiceFixture : IClassFixture<RecipesServices>
{
}