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
public class RolesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoles(CancellationToken token)
    {
        GetRolesQuery query = new();

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById([FromRoute] Guid id, CancellationToken token)
    {
        GetRoleByIdQuery query = new(id);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }
    
    [HttpGet("/admin/{id:guid}")]
    public async Task<IActionResult> IsUserAdmin([FromRoute] Guid id, CancellationToken token)
    {
        CheckIfAdminQuery query = new(id);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto dto, CancellationToken token)
    {
        CreateRoleCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }
}