using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;

namespace Recipes.Api.Controllers;

[ApiController]
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
    public async Task<IActionResult> UpdateIngredient([FromBody] IngredientEditDto dto, CancellationToken token)
    {
        //TODO: get user id
        UpdateIngredientCommand cmd = new(dto, Guid.NewGuid());

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
    public async Task<IActionResult> DeleteIngredient([FromRoute] Guid id, [FromRoute] Guid recipeId,
        CancellationToken token)
    {
        IngredientDeleteDto dto = new()
        {
            Id = id,
            RecipeId = recipeId
        };

        //TODO: get user id
        DeleteIngredientCommand cmd = new(dto, Guid.NewGuid());

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