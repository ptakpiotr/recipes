using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Handlers;
using Recipes.Application.Users.Queries;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class GetRoleByIdHandlerTests(RoleServices roleServices) : RoleServiceFixtures
{
    [Fact]
    public async Task GetRoleById_ShouldReturnRole()
    {
        var handler = new GetRoleByIdHandler(roleServices.SuccessRoleService);

        var param = new GetRoleByIdQuery(Guid.NewGuid());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<RoleReadDto>);
    }

    [Fact]
    public async Task GetRoleById_ShouldReturnError()
    {
        var handler = new GetRoleByIdHandler(roleServices.FailureRoleService);

        var param = new GetRoleByIdQuery(Guid.NewGuid());

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}