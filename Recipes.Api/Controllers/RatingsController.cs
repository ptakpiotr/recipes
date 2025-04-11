using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.Api.Filters;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;

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
}