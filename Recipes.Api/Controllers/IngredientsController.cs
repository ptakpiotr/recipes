using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Filters;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Infrastructure.Common.Identity;

namespace Recipes.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class IngredientsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateIngredient([FromBody] IngredientCreateDto dto, CancellationToken token)
    {
        CreateIngredientCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpPut]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> UpdateIngredient([FromBody] IngredientEditDto dto, CancellationToken token)
    {
        var parseResult = Guid.TryParse(HttpContext.Items["UserId"]?.ToString(), out var userId);

        if (!parseResult)
        {
            return BadRequest();
        }

        UpdateIngredientCommand cmd = new(dto, userId);

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

    [HttpDelete("{id:guid}/recipe/{recipeId:guid}")]
    [ServiceFilter<GroundUserInfoFilter>]
    [Authorize(Policy = IdentityConstants.AuthzPolicy)]
    public async Task<IActionResult> DeleteIngredient([FromRoute] Guid id, [FromRoute] Guid recipeId,
        CancellationToken token)
    {
        IngredientDeleteDto dto = new()
        {
            Id = id,
            RecipeId = recipeId
        };

        var parseResult = Guid.TryParse(HttpContext.Items["UserId"]?.ToString(), out var userId);

        if (!parseResult)
        {
            return BadRequest();
        }

        DeleteIngredientCommand cmd = new(dto, userId);

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