using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.Commands;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Handlers;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class CreateUserHandlerTests(UserServices userServices) : UserServiceFixtures
{
    [Fact]
    public async Task CreateUser_NewUser_ShouldReturnSuccess()
    {
        CreateUserHandler handler = new(userServices.SuccessUserService);

        var param = new CreateUserCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<UserReadDto>);
    }

    [Fact]
    public async Task CreateUser_NewUser_ShouldReturnError()
    {
        CreateUserHandler handler = new(userServices.FailureUserService);

        var param = new CreateUserCommand(new());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}