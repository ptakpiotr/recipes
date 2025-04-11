using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Recipes.Application.Users.Services;

namespace Recipes.Api.Filters;

public class GroundUserInfoFilter(IUserService userService) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idClaim = context.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "id");

        if (idClaim is null)
        {
            context.Result = new BadRequestResult();
            return;
        }

        var user = await userService.GetUserByExternalIdAsync(idClaim.Value, CancellationToken.None)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (user.IsT1)
        {
            context.Result = new BadRequestResult();
            return;
        }

        context.HttpContext.Items["UserId"] = user.AsT0.Value.Id; 
        
        await next();
    }
}