using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Filters;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Queries;

namespace Recipes.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class RatingsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> CreateRating([FromBody] RatingCreateDto dto, CancellationToken token)
    {
        dto.UserId = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);

        CreateRatingCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }

    [HttpPut]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> UpdateRating([FromBody] RatingEditDto dto, CancellationToken token)
    {
        var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);

        UpdateRatingCommand cmd = new(dto, userId);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<IActionResult>(_ => NoContent(), BadRequest);

        return actionRes;
    }

    [HttpDelete("{id:guid}")]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> DeleteRating([FromRoute] Guid id, CancellationToken token)
    {
        var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);

        DeleteRatingCommand cmd = new(new()
        {
            Id = id,
        }, userId);

        var res = await sender.Send(cmd, token);

        var actionRes = res.Match<StatusCodeResult>(status =>
        {
            if (status.Status)
            {
                return Ok();
            }

            return BadRequest();
        }, (_) => BadRequest());

        return actionRes;
    }
    
    [HttpGet("ownership/{ratingId:guid}")]
    [Authorize]
    [ServiceFilter<GroundUserInfoFilter>]
    public async Task<IActionResult> CheckOwnership([FromRoute] Guid ratingId,
        CancellationToken token)
    {
        var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString() ?? string.Empty);
        CheckRatingOwnershipQuery query = new(userId, ratingId);

        var res = await sender.Send(query, token);

        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }
}