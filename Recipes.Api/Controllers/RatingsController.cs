using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RatingsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRating([FromBody] RatingCreateDto dto, CancellationToken token)
    {
        CreateRatingCommand cmd = new(dto);

        var res = await sender.Send(cmd, token);
        
        var actionRes = res.Match<ObjectResult>(Ok, BadRequest);

        return actionRes;
    }
}