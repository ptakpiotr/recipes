using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Users.Commands;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Queries;
using Recipes.Infrastructure.Common.Identity;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize(Policy = IdentityConstants.AdminPolicy)]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        GetUsersQuery query = new();

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken token)
    {
        GetUserByIdQuery query = new(id);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = IdentityConstants.AdminPolicy)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken token)
    {
        UserDeleteDto dto = new()
        {
            Id = id
        };
        DeleteUserCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ActionResult>((commandStatus) =>
        {
            if (commandStatus.Status)
            {
                return NoContent();
            }

            return BadRequest();
        }, BadRequest);

        return actionRes;
    }

    [HttpPut]
    [Authorize(Roles = IdentityConstants.AdminPolicy)]
    public async Task<IActionResult> UpdateUser([FromBody] UserEditDto dto, CancellationToken token)
    {
        UpdateUserCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ActionResult>((commandStatus) =>
        {
            if (commandStatus.Status)
            {
                return NoContent();
            }

            return BadRequest();
        }, BadRequest);

        return actionRes;
    }
}