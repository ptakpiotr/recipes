namespace Recipes.Application.Users.Mappers;

public class UsersMapperProfile : Profile
{
    public UsersMapperProfile()
    {
        CreateMap<UserModel, UserReadDto>();
        CreateMap<UserModel, UserBasicReadDto>();
        CreateMap<RoleModel, RoleReadDto>();

        CreateMap<UserCreateDto, UserModel>();
        CreateMap<RoleCreateDto, RoleModel>();
    }
}