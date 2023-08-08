using AutoMapper;

namespace net_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<User, GetUserDto>();
            CreateMap<RequestUserDto, User>();
            CreateMap<User, RequestUserDto>();
        }
    }
}