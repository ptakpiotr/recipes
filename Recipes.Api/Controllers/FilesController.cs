using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Recipes.Application.Common.Services;

namespace Recipes.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FilesController(IFileService fileService) : ControllerBase
{
    [HttpGet("{fileName}")]
    [OutputCache(Duration = 60000)]
    public async Task<IActionResult> GetFile([FromRoute] string fileName, CancellationToken cancellationToken)
    {
        var stream = await fileService.GetFileContentsAsync(fileName, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (stream is null)
        {
            return NotFound();
        }

        return Ok(stream);
    }
}