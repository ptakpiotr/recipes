namespace Recipes.Application.Users.Commands;

public record CreateRoleCommand(RoleCreateDto Role): IRequest<OneOf<SuccessWithValue<RoleReadDto>, Error>>;