namespace Recipes.Application.Users.Queries;

public record GetRolesQuery : IRequest<OneOf<SuccessWithValue<IReadOnlyList<RoleReadDto>>, Error>>;