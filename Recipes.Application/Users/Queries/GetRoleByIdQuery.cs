namespace Recipes.Application.Users.Queries;

public record GetRoleByIdQuery(Guid RoleId) : IRequest<OneOf<SuccessWithValue<RoleReadDto>, Error>>;