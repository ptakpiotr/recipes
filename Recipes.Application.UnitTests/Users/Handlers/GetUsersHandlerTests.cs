using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Handlers;
using Recipes.Application.Users.Queries;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class GetUsersHandlerTests(UserServices userServices) : UserServiceFixtures
{
    [Fact]
    public async Task GetUsers_ReturnsUsers()
    {
        var handler = new GetUsersHandler(userServices.SuccessUserService);

        var param = new GetUsersQuery();

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<IReadOnlyList<UserReadDto>>);
    }

    [Fact]
    public async Task GetUsers_ReturnsError()
    {
        var handler = new GetUsersHandler(userServices.FailureUserService);

        var param = new GetUsersQuery();

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}