namespace Recipes.Application.Users.Commands;

public record CreateUserCommand(UserCreateDto User): IRequest<OneOf<SuccessWithValue<UserReadDto>, Error>>;