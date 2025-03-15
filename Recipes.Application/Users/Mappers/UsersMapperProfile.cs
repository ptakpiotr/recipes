namespace Recipes.Application.Users.Mappers;

public class UsersMapperProfile : Profile
{
    public UsersMapperProfile()
    {
        CreateMap<UserModel, UserReadDto>();
        CreateMap<RoleModel, RoleReadDto>();

        CreateMap<UserCreateDto, UserModel>();
        CreateMap<RoleCreateDto, RoleModel>();
    }
}