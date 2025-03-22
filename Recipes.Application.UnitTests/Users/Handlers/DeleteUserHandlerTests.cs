using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.Commands;
using Recipes.Application.Users.Handlers;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class DeleteUserHandlerTests(UserServices userServices) : UserServiceFixtures
{
    [Fact]
    public async Task DeleteUser_ProperUser_ShouldReturnSuccess()
    {
        DeleteUserHandler handler = new(userServices.SuccessUserService);

        var param = new DeleteUserCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }

    [Fact]
    public async Task DeleteUser_InvalidUser_ShouldReturnError()
    {
        DeleteUserHandler handler = new(userServices.FailureUserService);

        var param = new DeleteUserCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }
}