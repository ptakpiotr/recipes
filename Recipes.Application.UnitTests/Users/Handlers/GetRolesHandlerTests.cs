using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Handlers;
using Recipes.Application.Users.Queries;
using Recipes.Domain.Common.Results;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class GetRolesHandlerTests(RoleServices roleServices) : RoleServiceFixtures
{
    [Fact]
    public async Task GetRoles_ShouldReturnRoles()
    {
        var handler = new GetRolesHandler(roleServices.SuccessRoleService);

        var param = new GetRolesQuery();

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<IReadOnlyList<RoleReadDto>>);
    }

    [Fact]
    public async Task GetRoles_ShouldReturnError()
    {
        var handler = new GetRolesHandler(roleServices.FailureRoleService);

        var param = new GetRolesQuery();

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}