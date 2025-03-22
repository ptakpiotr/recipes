using NSubstitute;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Users.Handlers.Fixtures;

public class UserServices
{
    public IUserService SuccessUserService { get; } = Substitute.For<IUserService>();

    public IUserService FailureUserService { get; } = Substitute.For<IUserService>();
    

    public UserServices()
    {
        #region success

        SuccessUserService.GetUserByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<UserReadDto>(new UserReadDto()
            {
                Id = Guid.NewGuid(),
                UserEmail = "test@test.com",
                UserName = "test",
                UserImageLink = "https://bing.com"
            }));

        SuccessUserService.GetUserByExternalIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<UserReadDto>(new UserReadDto()
            {
                Id = Guid.NewGuid(),
                UserEmail = "test@test.com",
                UserName = "test",
                UserImageLink = "https://bing.com"
            }));

        SuccessUserService.GetUsersForNewseletterAsync(Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IReadOnlyList<UserReadDto>>([
                new UserReadDto()
                {
                    Id = Guid.NewGuid(),
                    UserEmail = "test@test.com",
                    UserName = "test",
                    UserImageLink = "https://bing.com"
                }
            ]));

        SuccessUserService.GetAllUsersAsync(Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IReadOnlyList<UserReadDto>>([
                new UserReadDto()
                {
                    Id = Guid.NewGuid(),
                    UserEmail = "test@test.com",
                    UserName = "test",
                    UserImageLink = "https://bing.com"
                }
            ]));

        SuccessUserService.CreateUserAsync(Arg.Any<UserCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<UserReadDto>(new UserReadDto()
            {
                Id = Guid.NewGuid(),
                UserEmail = "test@test.com",
                UserName = "test",
                UserImageLink = "https://bing.com"
            }));

        SuccessUserService.UpdateUserAsync(Arg.Any<UserEditDto>(), Arg.Any<CancellationToken>())
            .Returns(new Success());

        SuccessUserService.DeleteUserAsync(Arg.Any<UserDeleteDto>(), Arg.Any<CancellationToken>())
            .Returns(new Success());

        #endregion

        #region failure

        FailureUserService.GetUserByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        FailureUserService.GetUserByExternalIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        FailureUserService.GetUsersForNewseletterAsync(Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        FailureUserService.GetAllUsersAsync(Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        FailureUserService.CreateUserAsync(Arg.Any<UserCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        FailureUserService.UpdateUserAsync(Arg.Any<UserEditDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        FailureUserService.DeleteUserAsync(Arg.Any<UserDeleteDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Test"));

        #endregion
    }
}

public class UserServiceFixtures : IClassFixture<UserServices>
{
}