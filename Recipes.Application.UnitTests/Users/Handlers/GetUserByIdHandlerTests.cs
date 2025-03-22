using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Handlers;
using Recipes.Application.Users.Queries;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class GetUserByIdHandlerTests(UserServices userServices) : UserServiceFixtures
{
    [Fact]
    public async Task GetUserByIdAsync_ExistingUser_ShouldReturnSuccess()
    {
        GetUserByIdHandler handler = new(userServices.SuccessUserService);

        var param = new GetUserByIdQuery(Guid.NewGuid());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<UserReadDto>);
    }

    [Fact]
    public async Task CreateUser_NewUser_ShouldReturnError()
    {
        GetUserByIdHandler handler = new(userServices.FailureUserService);

        var param = new GetUserByIdQuery(Guid.NewGuid());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}