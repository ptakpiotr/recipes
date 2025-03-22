using Recipes.Application.UnitTests.Users.Handlers.Fixtures;
using Recipes.Application.Users.Commands;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Handlers;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Users.Enums;

namespace Recipes.Application.UnitTests.Users.Handlers;

public class CreateRoleHandlerTests(RoleServices roleServices) : RoleServiceFixtures
{
    [Fact]
    public async Task CreateRole_RoleExists_ShouldReturnSuccess()
    {
        CreateRoleHandler handler = new(roleServices.SuccessRoleService);

        var param = new CreateRoleCommand(new()
        {
            Name = "Test",
            Role = RoleType.User
        });

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is SuccessWithValue<RoleReadDto>);
    }

    [Fact]
    public async Task CreateRole_RoleDoesNotExist_ShouldReturnError()
    {
        CreateRoleHandler handler = new(roleServices.FailureRoleService);

        var param = new CreateRoleCommand(new()
        {
            Name = "Test",
            Role = RoleType.User
        });

        var res = await handler.Handle(param, CancellationToken.None);

        Assert.True(res.Value is Error);
    }
}