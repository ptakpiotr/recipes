using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Recipes.Api.Filters;
using Recipes.Api.Services;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Queries;
using Recipes.Domain.Recipes.Enums;
using Recipes.Infrastructure.Common.Identity;
using Recipes.Infrastructure.Common.Options;
using WebPush;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RecipesController(ISender sender, IWebPushService webPushService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRecipesAsync([FromQuery] RecipeType? filterType, CancellationToken token)
    {
        GetAllRecipesQuery query = new(filterType);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecipeByIdAsync([FromRoute] Guid id, CancellationToken token)
    {
        GetRecipeByIdQuery query = new(id);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpGet("ownership/{recipeId:guid}")]
    [Authorize]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> CheckOwnership([FromRoute] Guid recipeId,
        CancellationToken token)
    {
        var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);
        CheckOwnershipQuery query = new(userId, recipeId);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpPost]
    [Authorize]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> CreateRecipe([FromForm] RecipeCreateDto dto, CancellationToken token)
    {
        dto.AuthorId = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);
        CreateRecipeCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        await webPushService.SendPushNotificationAsync("Nowy wpis zostal utworzony", token);

        return actionRes;
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = IdentityConstants.AuthzPolicy)]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> DeleteRecipe([FromRoute] Guid id, CancellationToken token)
    {
        RecipeDeleteDto dto = new()
        {
            Id = id
        };

        var parseResult = Guid.TryParse(HttpContext.Items["UserId"]?.ToString(), out var userId);

        if (!parseResult)
        {
            return BadRequest();
        }

        DeleteRecipeCommand cmd = new(dto, userId);

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
    [Authorize(Policy = IdentityConstants.AuthzPolicy)]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> UpdateRecipe([FromBody] RecipeEditDto dto, CancellationToken token)
    {
        var parseResult = Guid.TryParse(HttpContext.Items["UserId"]?.ToString(), out var userId);

        if (!parseResult)
        {
            return BadRequest();
        }

        UpdateRecipeCommand cmd = new(dto, userId);

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