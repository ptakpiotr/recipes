using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Queries;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RecipesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRecipesAsync(CancellationToken token)
    {
        GetAllRecipesQuery query = new();

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

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateDto dto, CancellationToken token)
    {
        CreateRecipeCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] Guid id, CancellationToken token)
    {
        RecipeDeleteDto dto = new()
        {
            Id = id
        };

        //TODO: get user id
        DeleteRecipeCommand cmd = new(dto, Guid.NewGuid());

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
    public async Task<IActionResult> UpdateRecipe([FromBody] RecipeEditDto dto, CancellationToken token)
    {
        //TODO: get user id
        UpdateRecipeCommand cmd = new(dto, Guid.NewGuid());

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