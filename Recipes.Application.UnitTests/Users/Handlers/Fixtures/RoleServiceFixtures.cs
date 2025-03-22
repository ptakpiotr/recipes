using NSubstitute;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Users.Enums;

namespace Recipes.Application.UnitTests.Users.Handlers.Fixtures;

public class RoleServices
{
    public IRoleService SuccessRoleService { get; } = Substitute.For<IRoleService>();
    public IRoleService FailureRoleService { get; } = Substitute.For<IRoleService>();

    public RoleServices()
    {
        #region success

        SuccessRoleService.GetRoleByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<RoleReadDto>(new()
            {
                Name = "Success",
                Role = RoleType.User
            }));

        SuccessRoleService.GetAllRolesAsync(Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<IReadOnlyList<RoleReadDto>>([
                new()
                {
                    Name = "Success",
                    Role = RoleType.User
                }
            ]));

        SuccessRoleService.CreateRoleAsync(Arg.Any<RoleCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new SuccessWithValue<RoleReadDto>(new()
            {
                Name = "Success",
                Role = RoleType.User
            }));

        #endregion

        #region failure

        FailureRoleService.GetRoleByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Failure"));

        FailureRoleService.GetAllRolesAsync(Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Failure"));

        FailureRoleService.CreateRoleAsync(Arg.Any<RoleCreateDto>(), Arg.Any<CancellationToken>())
            .Returns(new Error(ErrorType.OperationFailed, "Failure"));

        #endregion
    }
}

public class RoleServiceFixtures : IClassFixture<RoleServices>
{
}