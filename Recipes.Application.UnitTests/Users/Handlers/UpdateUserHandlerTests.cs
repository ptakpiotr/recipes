using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.Commands;
using Recipes.Application.Users.Handlers;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class UpdateUserHandlerTests(UserServices userServices) : UserServiceFixtures
{
    [Fact]
    public async Task UpdateUser_UpdatesUser()
    {
        var handler = new UpdateUserHandler(userServices.SuccessUserService);

        var param = new UpdateUserCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }

    [Fact]
    public async Task UpdateUser_DoesntUpdateUser()
    {
        var handler = new UpdateUserHandler(userServices.FailureUserService);

        var param = new UpdateUserCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.IsT0);
    }
}