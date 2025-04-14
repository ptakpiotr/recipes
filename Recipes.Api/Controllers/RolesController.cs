using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Filters;
using Recipes.Application.Users.Commands;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Queries;
using Recipes.Infrastructure.Common.Identity;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RolesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = IdentityConstants.AdminPolicy)]
    public async Task<IActionResult> GetRoles(CancellationToken token)
    {
        GetRolesQuery query = new();

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = IdentityConstants.AdminPolicy)]
    public async Task<IActionResult> GetRoleById([FromRoute] Guid id, CancellationToken token)
    {
        GetRoleByIdQuery query = new(id);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }
    
    [HttpGet("admin")]
    [Authorize]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> IsUserAdmin(CancellationToken token)
    {
        var id = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);
        CheckIfAdminQuery query = new(id);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpPost]
    [Authorize(Policy = IdentityConstants.AdminPolicy)]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto dto, CancellationToken token)
    {
        CreateRoleCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }
}